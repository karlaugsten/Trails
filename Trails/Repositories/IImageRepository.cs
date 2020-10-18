using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Trails.Repositories
{
    public interface IImageRepository
    {
        Task<FileProcessingTask> AddImageAsync(IFormFile image, int editId);

        Stream GetImageStream(string imageName);

        /// <summary>
        /// Gets the URL where the image can be accessed from.
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        string GetUrl(string imageName);
        Image Get(int imageId);

        Task<FileProcessingTask> GetProcessingTask(int fileId);


    }
}