using System;
using System.IO;
using System.Threading.Tasks;
/// <summary>
/// A class to save file streams to a location.
/// </summary>
public interface IFileRepository
{
  /// <summary>
  /// Saves the file to a repository, and returns the url to access the file.
  /// </summary>
  /// <param name="fileType"></param>
  /// <param name="fileStream"></param>
  /// <returns></returns>
  string Save(String fileType, Stream fileStream);

/// <summary>
  /// Saves the file to a repository, and returns the url to access the file.
  /// </summary>
  /// <param name="fileType"></param>
  /// <param name="fileStream"></param>
  /// <returns></returns>
  Task<string> SaveAsync(String fileType, Stream fileStream);

  /// <summary>
  /// Gets the file.
  /// </summary>
  /// <param name="fileType"></param>
  /// <param name="fileStream"></param>
  /// <returns></returns>
  Stream Get(string fileName);

  /// <summary>
  /// Gets Url where the file can be accessed.
  /// </summary>
  /// <param name="fileType"></param>
  /// <param name="fileStream"></param>
  /// <returns></returns>
  String GetUrl(string fileName);
}