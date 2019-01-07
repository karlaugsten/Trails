using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class TrailEdit : BaseTrail {
  [Key]
  public int EditId { get; set; }
  public int TrailId { get; set; }
  /// <summary>
  /// TODO: Every edit must be associated with a user.
  /// Users will be able to pull up a list of edits they are working on.
  /// These can be approved/deleted.
  /// </summary>
  /// <value></value>
  //public int UserId { get; set; }
  public List<Image> Images { get; set; } = new List<Image>();

}