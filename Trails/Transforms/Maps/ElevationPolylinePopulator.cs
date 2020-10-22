
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class ElevationPolylinePopulator : IEndTransform<string, MapJobContext>
  {
    private TrailContext _context;

    public ElevationPolylinePopulator(TrailContext context) {
      _context = context;
    }

    public void transform(string input, MapJobContext context)
    {
      var map = _context.Maps.Find(context.mapId);
      if (map == null) throw new ArgumentException("No map found during processing");
      map.ElevationPolyline = input;
      _context.Maps.Update(map);
      _context.SaveChanges();
    }
  }
}