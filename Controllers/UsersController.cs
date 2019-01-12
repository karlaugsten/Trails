using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Trails.Controllers
{
  [Route("api/[controller]")]
  public class UsersController : Controller
  {
    private UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager) 
    {
      _userManager = userManager;
    }

    [HttpPost]
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
      return Ok(new { token = "test" });
    }
  }
}
