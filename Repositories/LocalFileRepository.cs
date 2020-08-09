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

  public string Save(String fileType, Action<Stream> saveFile)
  {
      var name = Guid.NewGuid().ToString();
      var fileName = name + fileType;
      var filePath = this.folder + fileName;

      using (Stream stream = new FileStream(filePath, FileMode.CreateNew))
      {
          saveFile(stream);
      }

      return fileName;
  }

  public async Task<string> SaveAsync(String fileType, Func<Stream, Task> saveFile)
  {
      var name = Guid.NewGuid().ToString();
      var fileName = name + fileType;
      var filePath = this.folder + fileName;

      using (Stream stream = new FileStream(filePath, FileMode.CreateNew))
      {
          await saveFile(stream);
      }
      return fileName;
  }

  public Stream Get(string fileName) {
    var filePath = this.folder + fileName;
    if(!System.IO.File.Exists(filePath)) { // TODO: Do the check in imagerepo somehow...
      throw new KeyNotFoundException("No image.");
    }
    FileStream fileStream = new FileStream(filePath, FileMode.Open);
    return fileStream;
  }
}