using System.Threading.Tasks;

namespace Trails.Authentication
{
    public interface ITokenFactory
    {
        Task<string> GenerateToken(User user);
    }
}