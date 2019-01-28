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
  public class UsersController : Controller
  {
    private UserManager<User> _userManager;
    private ITokenFactory _tokenFactory;

    public UsersController(UserManager<User> userManager, ITokenFactory tokenFactory) 
    {
      _userManager = userManager;
      _tokenFactory = tokenFactory;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginRequest login)
    {
      if(string.IsNullOrEmpty(login.Email) ||
        string.IsNullOrEmpty(login.Password))
      {
        return BadRequest();
      }
      var user = await _userManager.FindByEmailAsync(login.Email);
      if(user == null) return BadRequest();

      var valid = await _userManager.CheckPasswordAsync(user, login.Password);
      if(!valid) return BadRequest();
      return Ok(new { token = await _tokenFactory.GenerateToken(user) });
    }
  }
}
