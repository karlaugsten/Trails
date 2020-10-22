
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Trails.FileProcessing;

namespace Trails.Transforms {

  /// <summary>
  /// Converts the list of locations into a polyline to be saved in the database.
  /// </summary>
  public class ElevationPolylineConverter : ITransform<List<int>, string, MapJobContext>
  {
    public string transform(List<int> input, MapJobContext content)
    {
      return PolylineConverter.Convert(input);
    }
  }
}