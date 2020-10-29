using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trails.FileProcessing.Models;

namespace Trails.FileProcessing
{
  /// <summary>
  /// An interface to be implemented which handles all the interfacing between
  /// database and file operations.
  /// </summary>
  public interface IFileProcessingRepository
  {

    List<FileProcessing.Models.FileTransform> GetFiles();

    FileProcessing.Models.FileTransform SaveFile(FileProcessing.Models.FileTransform file);
    
    /// <summary>
    /// Gets all the names of the transforms that have been applied to this file.
    /// </summary>
    /// <returns></returns>
    Dictionary<string, List<string>> GetAppliedTransforms(int fileId);

    TransformJob SaveTransform(TransformJob transform);


    /// <summary>
    /// Save the list of transforms as applied to this file.
    /// </summary>
    /// <param name="fileId"></param>
    /// <param name="transforms"></param>
    void SaveTransforms(TransformJob transformJob, IEnumerable<string> transforms);

    void SetFileStatus(int fileId, FileStatus status);

    FileTransform GetFile(int fileId);

  }
}
