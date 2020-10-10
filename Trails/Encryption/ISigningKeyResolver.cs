using Microsoft.IdentityModel.Tokens;

namespace Trails.Encryption
{
    public interface ISigningKeyResolver
    {
        SigningCredentials GetSigningCredentials();
        SecurityKey GetSecurityKey();
    }
}