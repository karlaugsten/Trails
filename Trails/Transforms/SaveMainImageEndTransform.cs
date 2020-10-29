
using System;
using System.IO;
using Trails.FileProcessing;

namespace Trails.Transforms {

  /// <summary>
  /// Saves the image to s3 with a given file name,
  /// then updates the thumbnail image property on the image.
  /// </summary>
  public class SaveMainImageEndTransform : IEndTransform<string, ImageJobContext>
  {
    private TrailContext _context;

    public SaveMainImageEndTransform(TrailContext context) {
      _context = context;
    }

    public void transform(string input, ImageJobContext context)
    {
      var image = _context.Images.Find(context.imageId);
      image.Url = input;
      _context.Images.Update(image);
      _context.SaveChanges();
    }

    public async System.Threading.Tasks.Task transformAsync(string input, ImageJobContext context)
    {
      var image = _context.Images.Find(context.imageId);
      image.Url = input;
      _context.Images.Update(image);
      await _context.SaveChangesAsync();    
    }
  }
}