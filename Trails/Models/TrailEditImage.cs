using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Maps a trail edit to an image id.
/// </summary>
public class TrailEditImage {
  [ForeignKey("ImageId")]
  public Image Image { get; set; }
  public int ImageId { get; set; }
  public int EditId { get; set; }
  public TrailEdit Edit { get; set; }
}