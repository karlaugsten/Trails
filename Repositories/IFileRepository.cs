using System;
using System.IO;
using System.Threading.Tasks;
/// <summary>
/// A class to save file streams to a location.
/// </summary>
public interface IFileRepository
{
  string Save(String fileType, Stream fileStream);

  Task<string> SaveAsync(String fileType, Stream fileStream);

  Stream Get(string fileName);
}