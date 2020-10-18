
using System;
using System.IO;
using Trails.FileProcessing;

namespace Trails.Transforms {

  /// <summary>
  /// Saves the image to s3 with a given file name,
  /// then updates the thumbnail image property on the image.
  /// </summary>
  public class SaveJpgImageToS3Transform : ITransform<Stream, string, ImageJobContext>
  {
    private IFileRepository _fileRepository;

    public SaveJpgImageToS3Transform(IFileRepository fileRepository) {
      _fileRepository = fileRepository;
    }

    public string transform(Stream input, ImageJobContext context)
    {
      input.Position = 0;
      return _fileRepository.Save(".jpeg", input);
    }

    public async System.Threading.Tasks.Task<string> transformAsync(Stream input, ImageJobContext context)
    {
      input.Position = 0;
      return await _fileRepository.SaveAsync(".jpeg", input);
    }
  }
}