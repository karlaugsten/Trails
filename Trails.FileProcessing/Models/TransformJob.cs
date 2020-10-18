using System;

namespace Trails.FileProcessing.Models
{
  public class TransformJob {
    public int id { get; set; }
    public DateTime startTime {get; set;}
    public DateTime endTime {get; set;}

    public string transform {get;set;}
    public FileStatus status {get; set;}

    public string context { get; set; }

    public string input { get; set; }

    public int fileId { get; set; }
  }
}
