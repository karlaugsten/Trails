using System;

namespace Trails.FileProcessing.Models
{
  public class FileTransform {
    public int id {get;set;}
    public DateTime startTime {get; set;}
    public DateTime endTime {get; set;}

    public string fileType {get;set;}
    public FileStatus status {get; set;}

    /// <summary>
    /// Input to the file transform. Must be serializable.
    /// </summary>
    /// <value></value>
    public string context {get; set;}

    // The location of the file in s3.
    public string s3Location { get; set; }

    public string errorMessage { get; set; }
  }
}
