using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using System;
using SixLabors.Primitives;

public class ImageProcessor : IImageProcessor
{
  private void ResizeAndCrop(Image<Rgba32> image, int width, int height) {
    bool isWide = false;
    if(image.Width/image.Height > width/height) {
      isWide = true;
    }
    if(isWide) {
      
      double excessWidth = ((double)(image.Width)/(double)(image.Height))*height - width;
      image.Mutate(x => x
        .Resize(0, height)
        .Crop(Rectangle.FromLTRB((int)excessWidth/2, 0, width + (int)excessWidth/2, height)));
    } else {
      double excessHeight = ((double)(image.Height)/(double)(image.Width))*width - height;
      image.Mutate(x => x
        .Resize(width, 0)
        .Crop(Rectangle.FromLTRB(0, (int)(excessHeight/2.0), width, height + (int)(excessHeight/2.0))));
    }
  }

  /// <summary>
  /// This processes the file image to a jpeg image intended to be shown
  /// on the home page (400px x 250px size) as a jpeg format.
  /// 
  /// This then saves it to the passed in stream.
  /// </summary>
  /// <param name="imageFile"></param>
  /// <param name="stream"></param>
  /// <returns></returns>
  public async Task ProcessMainImageToStream(IFormFile imageFile, Stream stream)
  {
    // The goal MP to scale down to if needed.
    int standardMegaPixels = 3000000;
    // Image.Load(string path) is a shortcut for our default type. 
    // Other pixel formats use Image.Load<TPixel>(string path))
    using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(imageFile.OpenReadStream()))
    {
      int MP = image.Width * image.Height;

      if(MP > standardMegaPixels) {
        // Sqrt since we are scaling down height AND width;
        double scaleFactor = Math.Sqrt((double)standardMegaPixels/(double)MP);
        image.Mutate(x => x
          .Resize(0, (int)(image.Height * scaleFactor)));
      }
      // Save with 94% quality. The goal is to preserve the picture quality but only have ~1MB size image.
      image.SaveAsJpeg(stream, new JpegEncoder(){ 
        Quality = 94,
        IgnoreMetadata = true
      });
    }
  }

  /// <summary>
  /// This processes the file image to a jpeg image intended to be shown
  /// on the home page (400px x 250px size) as a jpeg format.
  /// 
  /// This then saves it to the passed in stream.
  /// </summary>
  /// <param name="imageFile"></param>
  /// <param name="stream"></param>
  /// <returns></returns>
  public async Task ProcessThumbnailImageToStream(IFormFile imageFile, Stream stream)
  {
    // Image.Load(string path) is a shortcut for our default type. 
    // Other pixel formats use Image.Load<TPixel>(string path))
    using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(imageFile.OpenReadStream()))
    {
      this.ResizeAndCrop(image, 800, 500);

      // Double the displayed size to allowed for better picture quality. 0 width to preserve aspect.
      image.SaveAsJpeg(stream, new JpegEncoder(){ 
        Quality = 90,
        IgnoreMetadata = true
      }); // Should still save to about 250Kb
     
    }
  }

  /// <summary>
  /// Returns a base64 string that encodes a tiny version of the image.
  /// 
  /// This will be in jpeg format.
  /// </summary>
  /// <param name="imageFile"></param>
  /// <returns></returns>
  public async Task<string> ProcessImageToBase64(IFormFile imageFile)
  {
    using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(imageFile.OpenReadStream()))
    {
      this.ResizeAndCrop(image, 40, 25);
      using(var memStream = new MemoryStream()) {
        image.SaveAsJpeg(memStream, new JpegEncoder(){ 
          Quality = 20, 
          IgnoreMetadata = true
        });
        byte[] buffer = new byte[memStream.Length];
        memStream.Position = 0;
        memStream.Read(buffer, 0, (int)memStream.Length);
        return Convert.ToBase64String(buffer);
      }
    }
  }
}