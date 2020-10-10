using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trails.Authentication;

namespace Trails.Controllers
{
  [Route("api/[controller]")]
  [Produces("application/json")]
  [ApiController]
  public class UsersController : Controller
  {
    private UserManager<User> _userManager;
    private ITokenFactory _tokenFactory;

    public UsersController(UserManager<User> userManager, ITokenFactory tokenFactory) 
    {
      _userManager = userManager;
      _tokenFactory = tokenFactory;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegistrationRequest registration)
    {
      if (string.IsNullOrEmpty(registration.Email))
      {
        return BadRequest(new RequestError("Please enter an email", "Email"));
      }
      if (string.IsNullOrEmpty(registration.ConfirmEmail))
      {
        return BadRequest(new RequestError("Please enter the email confirmation", "ConfirmEmail"));
      }
      if (registration.ConfirmEmail != registration.Email)
      {
        return BadRequest(new RequestError("Your email and email confirmation must match", "ConfirmEmail"));
      }
      if (string.IsNullOrEmpty(registration.Password))
      {
        return BadRequest(new RequestError("Please enter a password", "Password"));
      }
      if (string.IsNullOrEmpty(registration.ConfirmPassword))
      {
        return BadRequest(new RequestError("Please enter a password confirmation", "ConfirmPassword"));
      }
      if (registration.ConfirmPassword != registration.Password)
      {
        return BadRequest(new RequestError("Your passwords do not match", "ConfirmPassword"));
      }
      if (String.IsNullOrEmpty(registration.Username))
      {
        return BadRequest(new RequestError("Please enter a username", "Username"));
      }
      
      var existingUserByEmail = await _userManager.FindByEmailAsync(registration.Email);
      
      if (existingUserByEmail != null) {
        return BadRequest(new RequestError("A user with the email already exists", "Email"));
      }

      var existingUserByUserName = await _userManager.FindByNameAsync(registration.Username);
      
      if (existingUserByUserName != null) {
        return BadRequest(new RequestError("A user with the username already exists", "Username"));
      }
      var newUser = new User(){
        UserName = registration.Username,
        Email = registration.Email,
        EmailConfirmed = false
      };

      newUser.PasswordHash = _userManager.PasswordHasher.HashPassword(newUser, registration.Password);

      var result = await _userManager.CreateAsync(newUser);
      if(!result.Succeeded) {
        return BadRequest(new RequestError("Something went wrong, please contact us for help!"));
      }
      var emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
      var user = await _userManager.FindByEmailAsync(registration.Email);
      return Ok(new { token = emailToken, id = user.Id });
    }

    [HttpPost("confirm/{userId}/{token}")]
    public async Task<IActionResult> Confirm(string userId, string token) {
      var user = await _userManager.FindByIdAsync(userId);

      var result = await _userManager.ConfirmEmailAsync(user, token);
      if(result.Succeeded) {
        user.EmailConfirmed = true;
        var updateResult = await _userManager.UpdateAsync(user);
        if(updateResult.Succeeded) {
          return Redirect("https://runnify.ca/");
        }
      }
      // TODO: Redirect to error page.
      return BadRequest();
    } 

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest login)
    {
      if(string.IsNullOrEmpty(login.Email))
      {
        return BadRequest(new RequestError("Please enter an email", "Email"));
      }
      if(string.IsNullOrEmpty(login.Password))
      {
        return BadRequest(new RequestError("Please enter a password", "Password"));
      }

      var user = await _userManager.FindByEmailAsync(login.Email);
      if(user == null) return BadRequest(new RequestError("The email/password is invalid"));

      if(!user.EmailConfirmed) return BadRequest(new RequestError("Please confirm your email"));

      var valid = await _userManager.CheckPasswordAsync(user, login.Password);
      if(!valid) return BadRequest();
      return Ok(new { token = await _tokenFactory.GenerateToken(user) });
    }
  }
}
