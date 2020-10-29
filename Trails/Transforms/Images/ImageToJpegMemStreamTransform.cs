
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class ImageToJpegMemStreamTransform : ITransform<Image<Rgba32>, Stream, ImageJobContext>
  {
    private int _quality;
    
    public ImageToJpegMemStreamTransform(int quality) {
      _quality = quality;
    }

    public Stream transform(Image<Rgba32> input, ImageJobContext context)
    {
      var memStream = new MemoryStream();
      input.SaveAsJpeg(memStream, new JpegEncoder(){ 
        Quality = _quality,
        IgnoreMetadata = true
      });
      return memStream;
    }

    public async Task<Stream> transformAsync(Image<Rgba32> input, ImageJobContext context)
    {
      var memStream = new MemoryStream();
      input.SaveAsJpeg(memStream, new JpegEncoder(){ 
        Quality = _quality,
        IgnoreMetadata = true
      });
      return memStream;
    }
  }
}