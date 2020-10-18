
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class StreamToImageTransform : ITransform<Stream, Image<Rgba32>, ImageJobContext>
  {
    public StreamToImageTransform() {
    }

    public Image<Rgba32> transform(Stream input, ImageJobContext context)
    {
      var image = SixLabors.ImageSharp.Image.Load(input);
      input.Dispose();
      return image;
    }

    public async System.Threading.Tasks.Task<Image<Rgba32>> transformAsync(Stream input, ImageJobContext context)
    {
      var image = SixLabors.ImageSharp.Image.Load(input);
      await input.DisposeAsync();
      return image;
    }
  }
}