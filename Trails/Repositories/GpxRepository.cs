
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Trails.FileProcessing;
using Trails.FileProcessing.Models;
using Trails.Transforms;

public class GpxRepository : IGpxRepository
{
  private IFileRepository _fileRepository;

  private TrailContext _context;

  private ILogger<GpxRepository> _logger;

  private FileProcessor _fileProcessor;

  public GpxRepository(TrailContext context, FileProcessor fileProcessor, ILoggerFactory factory, IFileRepository fileRepository) {
    _context = context;
    _fileRepository = fileRepository;
    _fileProcessor = fileProcessor;
    _logger = factory.CreateLogger<GpxRepository>();
  }

  public FileProcessingTask GetFileStatus(int fileId)
  {
    var file = _fileProcessor.GetFile(fileId);
    if (file == null) throw new KeyNotFoundException();
    var task = new FileProcessingTask() {
      CallbackUrl = $"/maps/files/{fileId}",
      Status = file.status.ToString(),
      FinishedUrl = $"/maps/{file.context}"
    };
    var map = _context.Maps.Where(m => m.FileId == fileId).FirstOrDefault();
    if (map == null) throw new KeyNotFoundException();

    if (file.status == Trails.FileProcessing.Models.FileStatus.DONE) {
      task.FinishedUrl = $"/api/maps/{map.Id}";
    }

    return task;
  }

  public Map GetMap(int mapId)
  {
    return _context.Maps.Find(mapId);
  }

  public async Task<FileProcessingTask> UploadMap(Stream gpxFileStream, int editId)
  {
    var edit = await _context.TrailEdits.FindAsync(editId);
    if (edit == null) throw new KeyNotFoundException("Edit does not exist");

    // Now we need to convert the location list into a polyline to save it to the db
    string originalFileUrl = await _fileRepository.SaveAsync(".gpx", gpxFileStream);
    string name = originalFileUrl.Split("/").Last();
    Map map = new Map()
    {
      RawFileUrl = originalFileUrl
    };

    var entity = _context.Maps.Add(map);
    _context.SaveChanges();

    edit.MapId = entity.Entity.Id;
    _context.TrailEdits.Update(edit);
    _context.SaveChanges();

    FileTransform transform = await _fileProcessor.process<MapJobContext>("MapFile", name, new MapJobContext() {
      mapId = entity.Entity.Id
    });

    return new FileProcessingTask() {
      CallbackUrl = $"/maps/files/{transform.id}",
      Status = transform.status.ToString(),
      FinishedUrl = $"/maps/{entity.Entity.Id}"
    };
  }
}