
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using Trails.FileProcessing;

namespace Trails.Transforms {

  /// <summary>
  /// Resize the image to a specified dimension.
  /// </summary>
  public abstract class ImageResizeAndCropTransform : ITransform<Image<Rgba32>, Image<Rgba32>, ImageJobContext>
  {
    private int _width;
    private int _height;

    protected ImageResizeAndCropTransform(int width, int height) {
      _width = width;
      _height = height;
    }

    public Image<Rgba32> transform(Image<Rgba32> input, ImageJobContext context)
    {
      ResizeAndCrop(input, _width, _height);
      return input;
    }

    public async System.Threading.Tasks.Task<Image<Rgba32>> transformAsync(Image<Rgba32> input, ImageJobContext context)
    {
      ResizeAndCrop(input, _width, _height);
      return input;
    }

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
  }
}