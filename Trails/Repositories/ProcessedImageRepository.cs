using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Trails.FileProcessing;
using Trails.Transforms;

namespace Trails.Repositories
{
  /// <summary>
  /// An image repository implementation that saves images to disk somewhere.
  /// </summary>
  public class ProcessedImageRepository : IImageRepository
  {
    private TrailContext _context;
    private FileProcessor _imageProcessor;

    private IFileRepository _fileRepository;

    private Dictionary<string, string> MimeTypeMap = new Dictionary<string, string>() {
      {"image/jpeg", ".jpeg"},
      {"image/png", ".png"},
      {"image/tiff", ".tiff"}
    };

    public ProcessedImageRepository(IFileRepository fileRepository, TrailContext context, FileProcessor imageProcessor)
    {
      _context = context;
      _fileRepository = fileRepository;
      _imageProcessor = imageProcessor;
    }
    
    public async Task<FileProcessingTask> AddImageAsync(IFormFile image, int editId) {
      // Save the image to wherever we are storing it.. for now just the file system.
      var fileType = this.MimeTypeMap[image.ContentType];
      string originalImageUrl = null;
      originalImageUrl = await _fileRepository.SaveAsync(fileType, image.OpenReadStream());
      string name = originalImageUrl.Split("/").Last();
      var newImage = new Image() {
        Name = originalImageUrl.Split("/").Last(),
        EditId = editId,
      };

      _context.Images.Add(newImage);
      _context.SaveChanges();

      var file = await _imageProcessor.process("TrailImage", name, new ImageJobContext() {
        imageId = newImage.Id
      });

      newImage.fileId = file.id;
      _context.Images.Update(newImage);
      _context.TrailEditImages.Add(new TrailEditImage() {
        ImageId = newImage.Id,
        EditId = editId
      });
      _context.SaveChanges();

      return new FileProcessingTask() {
        CallbackUrl = $"/api/images/file/{file.id}",
        Status = file.status.ToString()
      };
    }

    public async Task<FileProcessingTask> GetProcessingTask(int fileId) {
      var file = _imageProcessor.GetFile(fileId);
      if (file == null) throw new KeyNotFoundException();
      var task = new FileProcessingTask() {
        CallbackUrl = $"/api/images/file/{file.id}",
        Status = file.status.ToString()
      };
      var image = _context.Images.Where(i => i.fileId == fileId).FirstOrDefault();
      if (image == null) throw new KeyNotFoundException();
      if (file.status == FileProcessing.Models.FileStatus.DONE) {
        task.FinishedUrl = $"/api/images/data/{image.Id}";
      }
      if (file.status == FileProcessing.Models.FileStatus.ERRORED) {
        task.ErrorMessage = file.errorMessage;
      }
      return task;
    }

    public Stream GetImageStream(string imageName) => _fileRepository.Get(imageName);

    public string GetUrl(string imageName) => _fileRepository.GetUrl(imageName);

    public Image Get(int imageId) => _context.Images.Find(imageId);
  }
}