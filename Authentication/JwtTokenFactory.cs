using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Trails.Authentication
{
    public class JwtTokenFactory : ITokenFactory
    {
        private ISigningKeyResolver _signingKey;
        private ILogger<JwtTokenFactory> _logger;
        private int _tokenExpiryInMinutes;

        public JwtTokenFactory(ISigningKeyResolver signingKey, ILogger<JwtTokenFactory> logger)
        {
            _signingKey = signingKey;
            _logger = logger;
            _tokenExpiryInMinutes = 1000;
        }

        public string GenerateToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Email, user.Email ?? ""),
                        new Claim(ClaimTypes.Name, user.UserName ?? "")
                    }),
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