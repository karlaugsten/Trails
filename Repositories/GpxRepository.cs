
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

public class GpxRepository : IGpxRepository
{
  private IFileRepository _fileRepository;

  private TrailContext _context;

  public GpxRepository(TrailContext context, IConfiguration config) {
    _context = context;
    _fileRepository = new LocalFileRepository(config["GpxRepoFolder"]);
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
    
    // Now we need to convert the location list into a polyline to save it to the db
    Map map = new Map()
    {
      Start = gpsPoints.First(),
      End = gpsPoints.Last(),
      Polyline = PolylineConverter.Convert(gpsPoints),
      // Can we read from this stream twice?
      RawFileUrl = "/api/maps/raw/" + _fileRepository.Save(".gpx", (stream) => gpxFileStream.CopyTo(stream))
    };
    var entity = _context.Maps.Add(map);
    _context.SaveChanges();
    return entity.Entity;
  }

  private List<Location> parseLocationFromTrackXml(XmlDocument gpx) {
    var gpsPoints = new List<Location>();

    foreach(XmlNode trk in gpx.SelectSingleNode("gpx").SelectNodes("trk")) {
      foreach(XmlNode segment in trk.SelectNodes("trkseg")) {
        var points = segment.SelectNodes("trkpt");
        foreach(XmlNode pointNode in points) {
          Location location = new Location()
          {
            Latitude = decimal.Parse(pointNode.Attributes["lon"].Value),
            Longitude = decimal.Parse(pointNode.Attributes["lon"].Value),
          };
          gpsPoints.Add(location);
        }
      }
    }
    return gpsPoints;
  }

  private List<Location> parseLocationFromRouteXml(XmlDocument gpx) {
    var gpsPoints = new List<Location>();

    foreach(XmlNode trk in gpx.SelectSingleNode("gpx").SelectNodes("rte")) {
      foreach(XmlNode pointNode in trk.SelectNodes("rtept")) {
        Location location = new Location()
        {
          Latitude = decimal.Parse(pointNode.Attributes["lon"].Value),
          Longitude = decimal.Parse(pointNode.Attributes["lon"].Value),
        };
        gpsPoints.Add(location);
      }
    }
    return gpsPoints;
  }
}