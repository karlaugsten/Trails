
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Extensions.Logging;
using Trails.FileProcessing;

namespace Trails.Transforms {

  public class XmlDocumentElevation20MeterStep : ITransform<XmlDocument, List<int>, MapJobContext>
  {
    private ILogger<XmlDocumentElevation20MeterStep> _logger;

    public XmlDocumentElevation20MeterStep(ILogger<XmlDocumentElevation20MeterStep> logger) {
      _logger = logger;
    }

    public List<int> transform(XmlDocument input, MapJobContext content)
    {
      XmlNamespaceManager nsmgr = new XmlNamespaceManager(input.NameTable);
      nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");     
      var elevationLocations = new List<Tuple<double, double>>();
      var eleNodes = input.SelectNodes("//x:ele", nsmgr);
      
      double distance = 0.0;
      Location lastLocation = null;
      foreach(XmlNode eleNode in eleNodes) {
        double elevation = double.Parse(eleNode.InnerText);
        var locationNode = eleNode.ParentNode;
        Location location = new Location()
        {
          Latitude = double.Parse(locationNode.Attributes["lat"].Value),
          Longitude = double.Parse(locationNode.Attributes["lon"].Value),
        };
        if(lastLocation != null) {
          double distanceFromLast = lastLocation.Distance(location);
          distance += distanceFromLast;
        }

        elevationLocations.Add(Tuple.Create(distance, elevation));
        lastLocation = location;
      }

      Interpolator<double, double> interpolator = new LinearInterpolator(
        elevationLocations.Select(tuple => tuple.Item1).ToArray(), 
        elevationLocations.Select(tuple => tuple.Item2).ToArray()
      );

      _logger.LogInformation("Total distance for GPX: " + distance);
      return interpolator.interpolateAll(0, distance, 20.0/1000.0)
        .Select(value => (int)Math.Round(value))
        .ToList();
      }
  }
}