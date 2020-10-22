
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Trails.FileProcessing;

namespace Trails.Transforms {

  /// <summary>
  /// Interpolates the location to every 50 meters.
  /// </summary>
  public class LocationInterpolator50MeterStep : ITransform<List<Location>, List<Location>, MapJobContext>
  {
    public List<Location> transform(List<Location> input, MapJobContext content)
    {
      var totalDistance = input.ToTotalDistance();
      var interpolator = new LocationInterpolator(input);
      return interpolator.interpolateAll(0, totalDistance, 50.0/1000.0).ToList();
    }
  }
}