
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class XmlDocumentToLocations : ITransform<XmlDocument, List<Location>, MapJobContext>
  {
    public List<Location> transform(XmlDocument input, MapJobContext content)
    {
      XmlNamespaceManager nsmgr = new XmlNamespaceManager(input.NameTable);
      nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");     
      var gpsPoints = new List<Location>();
      return input.SelectSingleNode("//x:gpx", nsmgr)
        .SelectNodes("//x:trkpt", nsmgr)
        .Cast<XmlNode>()
        .Select(n => new Location() {
          Latitude = double.Parse(n.Attributes["lat"].Value),
          Longitude = double.Parse(n.Attributes["lon"].Value),
        }).ToList();
    }
  }
}