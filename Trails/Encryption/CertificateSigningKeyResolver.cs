using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Trails.Encryption
{
    public class CertificateSigningKeyResolver : ISigningKeyResolver, IDecryptor, IEncryptor
    {
        private string _thumbprint;
        private X509Certificate2 _certificate;
        private RSA _privateKey;
        private RSA _publicKey;

        public CertificateSigningKeyResolver(string thumbprint)
        {
            _thumbprint = thumbprint;
            // pre-load the RSASecurityKey into memory for now, to improve performance...
            // Disadvantage to pre-loading is that any attackers could potentiall hex dump the memory and obtain
            // the private key.
            try
            {
                X509Store computerCaStore = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                computerCaStore.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection encryptionCertificate = computerCaStore.Certificates.Find(X509FindType.FindByThumbprint, _thumbprint, false);
                // Count of zero indicates certificate could not be found.
                if (encryptionCertificate.Count == 0)
                {
                    throw new ArgumentException("Could not find a valid encryption certificate with the thumbprint: " +
                        thumbprint +
                        " please make sure the correct certificate is installed on the machine.");
                }
                _certificate = encryptionCertificate[0];
                computerCaStore.Close();
                _privateKey = _certificate.GetRSAPrivateKey();
                _publicKey = _certificate.GetRSAPublicKey();
            }
            catch (Exception e)
            {
                throw new Exception("A fatal error occurred loading the signing certificate with thumbprint " +
                    thumbprint +
                    ". Please see inner exception for details.", e);
            }
        }

        public SigningCredentials GetSigningCredentials() => new SigningCredentials(new RsaSecurityKey(_privateKey), SecurityAlgorithms.RsaSha256);

        public SecurityKey GetSecurityKey() => new RsaSecurityKey(_privateKey);

        public X509Certificate2 GetCertificate() => _certificate;

        public string Decrypt(string toDecrypt) => Encoding.ASCII.GetString(_privateKey.Decrypt(Convert.FromBase64String(toDecrypt), RSAEncryptionPadding.OaepSHA1));

        public string Encrypt(string toEncrypt) => Convert.ToBase64String(_publicKey.Encrypt(Encoding.ASCII.GetBytes(toEncrypt), RSAEncryptionPadding.OaepSHA1));
    }
}