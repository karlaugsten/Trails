using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public interface IGpxRepository {

  Task<Map> UploadMap(Stream gpxFile);

  Map GetMap(int mapId);

}