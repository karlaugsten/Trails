
using System;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class LocationPolylinePopulator : IEndTransform<string, MapJobContext>
  {
    private TrailContext _context;

    public LocationPolylinePopulator(TrailContext context) {
      _context = context;
    }

    public void transform(string input, MapJobContext context)
    {
      var map = _context.Maps.Find(context.mapId);
      if (map == null) throw new ArgumentException("No map found during processing");
      map.Polyline = input;
      _context.Maps.Update(map);
      _context.SaveChanges();
    }
  }
}