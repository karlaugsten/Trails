using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Trails.Encryption;

namespace Trails.Authentication
{
    public class JwtTokenFactory : ITokenFactory
    {
        private ISigningKeyResolver _signingKey;
        private ILogger<JwtTokenFactory> _logger;
        private int _tokenExpiryInMinutes;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<int>> _roleManager;

        public JwtTokenFactory(ISigningKeyResolver signingKey, 
            ILogger<JwtTokenFactory> logger, 
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _signingKey = signingKey;
            _logger = logger;
            _userManager = userManager;
            _tokenExpiryInMinutes = 1000;
            _roleManager = roleManager;
        }

        public async Task<string> GenerateToken(User user)
        {
            try
            {
                var allClaims = new List<Claim>();
                var userClaims = await _userManager.GetClaimsAsync(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                allClaims.AddRange(userClaims);
                foreach (var userRole in userRoles)
                {
                    allClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    var role = await _roleManager.FindByNameAsync(userRole);
                    if(role != null)
                    {
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        foreach(Claim roleClaim in roleClaims)
                        {
                            allClaims.Add(roleClaim);
                        }
                    }
                }
                allClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                allClaims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
                allClaims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(allClaims),
                    Expires = DateTime.UtcNow.AddMinutes(_tokenExpiryInMinutes),
                    SigningCredentials = _signingKey.GetSigningCredentials()
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while generating token for user " + user.UserName + ":");
                throw new Exception("An error occurred while generating the token.", ex);
            }
        }
    }
}