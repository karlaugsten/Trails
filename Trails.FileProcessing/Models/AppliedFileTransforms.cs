
namespace Trails.FileProcessing.Models
{
  public class AppliedFileTransforms {
    public int fileId { get; set; }
    public int transformJobId { get; set; }
    public string[] appliedTransforms { get; set; }
    public string transformName { get; set; }
  }
}
