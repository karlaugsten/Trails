using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public interface IImageProcessor {
  Task ProcessImageToStream(IFormFile image, Stream stream);
  Task<string> ProcessThumbnailImageToStream(IFormFile image, Stream stream);
}