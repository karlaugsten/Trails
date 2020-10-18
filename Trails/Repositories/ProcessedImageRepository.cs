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
    
    public async Task<Image> AddImageAsync(IFormFile image, int editId) {
      // Save the image to wherever we are storing it.. for now just the file system.
      var fileType = this.MimeTypeMap[image.ContentType];
      string originalImageUrl = null;
      originalImageUrl = await _fileRepository.SaveAsync(fileType, image.OpenReadStream());
      string name = originalImageUrl.Split("/").Last();
      var newImage = new Image() {
        Url = "TODO",
        ThumbnailUrl = "TODO",
        Name = originalImageUrl.Split("/").Last(),
        EditId = editId,
        Base64Preview = "TODO"
      };

      _context.Images.Add(newImage);

      _context.SaveChanges();

      var file = await _imageProcessor.process("TrailImage", name, new ImageJobContext() {
        imageId = newImage.Id
      });

      return newImage;
    }

    public Stream GetImageStream(string imageName) => _fileRepository.Get(imageName);

    public string GetUrl(string imageName) => _fileRepository.GetUrl(imageName);

  }
}