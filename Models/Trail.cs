using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Trail : BaseTrail
{
  [Key]
  public int TrailId { get; set; }
  /// <summary>
  /// Foreign key to the specific edit this trail came from.
  /// </summary>
  /// <value></value>
  public int? EditId { get; set; } = null;
  /// <summary>
  /// A flag indicating that this trail has been flagged as approved for
  /// viewing on the sites main page.
  /// </summary>
  /// <value></value>
  public bool Approved { get; set; }
}