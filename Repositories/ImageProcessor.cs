using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;

public class ImageProcessor : IImageProcessor
{
  public async Task<string> ProcessThumbnailImageToStream(IFormFile imageFile, Stream stream)
  {
    // Image.Load(string path) is a shortcut for our default type. 
      // Other pixel formats use Image.Load<TPixel>(string path))
      using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(imageFile.OpenReadStream()))
      {
          image.Mutate(x => x
              .Resize(0, 500)); // Double the displayed size to allowed for better picture quality. 0 width to preserve aspect.
          image.SaveAsJpeg(stream, new JpegEncoder(){ Quality = 95 }); // Should still save to about 250Kb
      }
      
      /*using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(imageFile.OpenReadStream()))
      {
        image.Mutate(x => x
              .Resize(0, 50));
              
          return image.ToBase64String(ImageFormats.Jpeg);
      } */
      return "";
  }
}