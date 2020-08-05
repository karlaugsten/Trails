using System.ComponentModel.DataAnnotations;

public class Map {
  [Key]
  public int Id { get; set; }
  public string Polyline { get; set; }
  
  public string ElevationPolyline { get; set; }

  public Location Start { get; set; }

  public Location End { get; set; }

  /// <summary>
  /// This is the URL where we save the raw .gpx file to (could be S3, or other.)
  /// </summary>
  /// <value></value>
  public string RawFileUrl { get; set; }
}