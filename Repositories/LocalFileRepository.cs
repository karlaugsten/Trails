using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class LocalFileRepository : IFileRepository
{
  private String folder;

  public LocalFileRepository(String folder) {
    this.folder = folder;
  }

  public string Save(String fileType, Stream fileStream)
  {
      var name = Guid.NewGuid().ToString();
      var fileName = name + fileType;
      var filePath = this.folder + fileName;

      using (Stream stream = new FileStream(filePath, FileMode.CreateNew))
      {
          fileStream.CopyTo(stream);
      }

      return GetLocalUrl(fileName);
  }

  public async Task<string> SaveAsync(String fileType, Stream fileStream)
  {
      var name = Guid.NewGuid().ToString();
      var fileName = name + fileType;
      var filePath = this.folder + fileName;

      using (Stream stream = new FileStream(filePath, FileMode.CreateNew))
      {
          await fileStream.CopyToAsync(stream);
      }
      return GetLocalUrl(fileName);
  }

  public Stream Get(string fileName) {
    var filePath = this.folder + fileName;
    if(!System.IO.File.Exists(filePath)) { // TODO: Do the check in imagerepo somehow...
      throw new KeyNotFoundException("No image.");
    }
    FileStream fileStream = new FileStream(filePath, FileMode.Open);
    return fileStream;
  }

  /// <summary>
  /// The URL where the client will first hit to get the image.
  /// </summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  private string GetLocalUrl(string fileName) => $"/api/images/{fileName}";

  /// <summary>
  /// Returns the URL for an endpoint to directly retrieve the files from the IFileRepository.
  /// </summary>
  /// <param name="fileName"></param>
  /// <returns></returns>
  public string GetUrl(string fileName) => $"/api/images/direct/{fileName}";
}