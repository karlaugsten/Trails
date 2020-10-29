using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public interface IGpxRepository {

  Task<FileProcessingTask> UploadMap(Stream gpxFile, int editId);

  Map GetMap(int mapId);

  FileProcessingTask GetFileStatus(int fileId);
}