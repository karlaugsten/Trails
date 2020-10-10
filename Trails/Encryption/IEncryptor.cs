namespace Trails.Encryption
{
    public interface IEncryptor
    {
        string Encrypt(string toEncrypt);
    }
}