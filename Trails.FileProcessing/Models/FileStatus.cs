using System;
using System.Threading.Tasks;

namespace Trails.FileProcessing.Models
{
  public enum FileStatus {
    DONE,
    QUEUED,
    UPLOADING,
    PROCESSING,
    ERRORED
  }
}
