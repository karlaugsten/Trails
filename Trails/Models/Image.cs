using System.ComponentModel.DataAnnotations;

public class Image {
  [Key]
  public int Id { get; set; }
  /// <summary>
  /// Foreign key to the specific trail edit this image belongs to.
  /// </summary>
  /// <value></value>
  public int EditId { get; set; }
  public string Name { get; set; }
  public string Url { get; set; }
  public string ThumbnailUrl { get; set; }

  public string Base64Preview { get; set; }
  public int fileId { get; set; }
}