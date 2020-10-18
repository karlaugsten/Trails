
using System;
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
  public class ImagePreserveAspectResizeTransform : ITransform<Image<Rgba32>, Image<Rgba32>, ImageJobContext>
  {
    /// <summary>
    /// Desired meagpixels for image to be.
    /// </summary>
    private int _pixels;

    public ImagePreserveAspectResizeTransform() {
      // 3MP
      _pixels = 3000000;
    }

    public Image<Rgba32> transform(Image<Rgba32> input, ImageJobContext context)
    {
      Resize(input);
      return input;
    }

    public async System.Threading.Tasks.Task<Image<Rgba32>> transformAsync(Image<Rgba32> input, ImageJobContext context)
    {
      Resize(input);
      return input;
    }

    private void Resize(Image<Rgba32> image) {
      int MP = image.Width * image.Height;

      if(MP > _pixels) {
        // Sqrt since we are scaling down height AND width;
        double scaleFactor = Math.Sqrt((double)_pixels/(double)MP);
        image.Mutate(x => x
          .Resize(0, (int)(image.Height * scaleFactor)));
      }
    }
  }
}