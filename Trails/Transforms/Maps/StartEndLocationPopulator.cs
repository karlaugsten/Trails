
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class StartEndLocationPopulator : IEndTransform<List<Location>, MapJobContext>
  {
    private TrailContext _context;

    public StartEndLocationPopulator(TrailContext context) {
      _context = context;
    }

    public void transform(List<Location> input, MapJobContext context)
    {
      var map = _context.Maps.Find(context.mapId);
      if (map == null) throw new ArgumentException("No map found during processing");
      if(input.Count() < 10) throw new ArgumentException("The GPX file does not contain data.");
      map.Start = input.First();
      map.End = input.Last();
      _context.Maps.Update(map);
      _context.SaveChanges();
    }
  }
}