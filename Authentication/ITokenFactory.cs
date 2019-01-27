namespace Trails.Authentication
{
    public interface ITokenFactory
    {
        string GenerateToken(User user);
    }
}