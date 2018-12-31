using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Trails.Repositories
{
    public interface IImageRepository
    {
        Task<Image> AddImageAsync(IFormFile image, int editId);
        Stream GetImageStream(string imageName);
    }
}