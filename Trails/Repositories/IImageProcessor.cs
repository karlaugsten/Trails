using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public interface IImageProcessor {

  Task ProcessMainImageToStream(IFormFile image, Stream stream);
  Task ProcessThumbnailImageToStream(IFormFile image, Stream stream);

  Task<string> ProcessImageToBase64(IFormFile image);

}