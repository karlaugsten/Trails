using System;
using System.IO;
using System.Threading.Tasks;
/// <summary>
/// A class to save file streams to a location.
/// </summary>
public interface IFileRepository
{
  string Save(String fileType, Action<Stream> saveFile);

  Task<string> SaveAsync(String fileType, Func<Stream, Task> saveFile);

  Stream Get(string fileName);
}