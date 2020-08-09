
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class GpxRepository : IGpxRepository
{
  private IFileRepository _fileRepository;

  private TrailContext _context;

  private ILogger<GpxRepository> _logger;

  public GpxRepository(TrailContext context, IConfiguration config, ILoggerFactory factory, IFileRepository fileRepository) {
    _context = context;
    _fileRepository = fileRepository;
    _logger = factory.CreateLogger<GpxRepository>();
  }

  public Map GetMap(int mapId)
  {
    return _context.Maps.Find(mapId);
  }

  public async Task<Map> UploadMap(Stream gpxFileStream)
  {
    XmlDocument gpxDoc = new XmlDocument();
    gpxDoc.Load(gpxFileStream);
       
    var gpsPoints = parseLocationFromTrackXml(gpxDoc);

    if(gpsPoints.Count == 0) {
      // try to parse from the route elements instead.
      gpsPoints = parseLocationFromRouteXml(gpxDoc);
    }

    var locationInterpolator = new LocationInterpolator(gpsPoints);
    var totalDistance = gpsPoints.ToTotalDistance();
    
    gpsPoints = locationInterpolator.interpolateAll(0, totalDistance, 50.0/1000.0).ToList();

    var elevation = parseElevationFromTrackXml(gpxDoc, 30);

    gpxFileStream.Seek(0, SeekOrigin.Begin);
    // Now we need to convert the location list into a polyline to save it to the db
    Map map = new Map()
    {
      Start = gpsPoints.First(),
      End = gpsPoints.Last(),
      Polyline = PolylineConverter.Convert(gpsPoints),
      ElevationPolyline = PolylineConverter.Convert(elevation),
      // Can we read from this stream twice?
      RawFileUrl = "/api/maps/raw/" + _fileRepository.Save(".gpx", gpxFileStream)
    };
    var entity = _context.Maps.Add(map);
    _context.SaveChanges();
    return entity.Entity;
  }

  private List<Location> parseLocationFromTrackXml(XmlDocument gpx) {
    XmlNamespaceManager nsmgr = new XmlNamespaceManager(gpx.NameTable);
    nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");     
    var gpsPoints = new List<Location>();
    var gpxNode = gpx.SelectSingleNode("//x:gpx", nsmgr);
    var points = gpxNode.SelectNodes("//x:trkpt", nsmgr);
    foreach(XmlNode pointNode in points) {
      Location location = new Location()
      {
        Latitude = double.Parse(pointNode.Attributes["lat"].Value),
        Longitude = double.Parse(pointNode.Attributes["lon"].Value),
      };
      gpsPoints.Add(location);
    }
    return gpsPoints;
  }

  private List<int> parseElevationFromTrackXml(XmlDocument gpx, int numSamplesPerKm) {
    XmlNamespaceManager nsmgr = new XmlNamespaceManager(gpx.NameTable);
    nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");     
    var elevationLocations = new List<Tuple<double, double>>();
    var eleNodes = gpx.SelectNodes("//x:ele", nsmgr);
    
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

  private List<Location> parseLocationFromRouteXml(XmlDocument gpx) {
    var gpsPoints = new List<Location>();
    XmlNamespaceManager nsmgr = new XmlNamespaceManager(gpx.NameTable);
    nsmgr.AddNamespace("x", "http://www.topografix.com/GPX/1/1");   
    foreach(XmlNode pointNode in gpx.SelectNodes("//x:rtept", nsmgr)) {
      Location location = new Location()
      {
        Latitude = double.Parse(pointNode.Attributes["lat"].Value),
        Longitude = double.Parse(pointNode.Attributes["lon"].Value),
      };
      gpsPoints.Add(location);
    }
    return gpsPoints;
  }
}