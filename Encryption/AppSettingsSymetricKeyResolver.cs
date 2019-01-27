using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Trails.Encryption
{
    /// <summary>
    /// Resolves a symmetric key that is stored in the appsettings, by the name of the appsetting.
    /// </summary>
    public class AppSettingsSymetricKeyResolver : ISigningKeyResolver
    {
        private IConfiguration _settings;
        private string _name;

        public AppSettingsSymetricKeyResolver(IConfiguration settings, string settingName)
        {
            _settings = settings;
            _name = settingName;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.ASCII.GetBytes(_settings[_name]);
            return new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        }

        public SecurityKey GetSecurityKey()
        {
            var key = Encoding.ASCII.GetBytes(_settings[_name]);
            return new SymmetricSecurityKey(key);
        }
    }
}