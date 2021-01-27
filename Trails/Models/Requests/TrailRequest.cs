using System;
using System.Collections.Generic;
using System.Linq;

public class TrailRequest : BaseTrail {
  public int TrailId { get; set; }
  public int EditId { get; set; }
  public IList<Image> Images { get; set; }
  public Map Map { get; set; }
  public Nullable<int> MapId { get; set; }

  public static TrailRequest fromEdit(TrailEdit edit) {
    return new TrailRequest() {
      Elevation = edit.Elevation,
      Location = edit.Location,
      Description = edit.Description,
      Distance = edit.Distance,
      Map = edit.Map,
      MapId = edit.MapId,
      MaxDuration = edit.MaxDuration,
      MinDuration = edit.MinDuration,
      MaxSeason = edit.MaxSeason,
      MinSeason = edit.MinSeason,
      Images = edit.Images.Select(im => im.Image).ToList(),
      Title = edit.Title,
      Rating = edit.Rating,
      EditId = edit.EditId,
      TrailId = edit.TrailId
    };
  }
}