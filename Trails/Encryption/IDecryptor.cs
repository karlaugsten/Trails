namespace Trails.Encryption
{
    public interface IDecryptor
    {
        string Decrypt(string toDecrypt);
    }
}