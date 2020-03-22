using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Trails.Repositories
{
  /// <summary>
  /// An image repository implementation that saves images to disk somewhere.
  /// </summary>
  public class FileImageRepository : IImageRepository
  {
    private TrailContext _context;
    private IImageProcessor _imageProcessor;

    private IFileRepository _fileRepository;

    private Dictionary<string, string> MimeTypeMap = new Dictionary<string, string>() {
      {"image/jpeg", ".jpeg"},
      {"image/png", ".png"},
      {"image/tiff", ".tiff"}
    };

    public FileImageRepository(TrailContext context, IConfiguration config, IImageProcessor imageProcessor)
    {
      _context = context;
      _fileRepository = new LocalFileRepository(config["ImageRepoFolder"]);
      _imageProcessor = imageProcessor;
    }
    
    public async Task<Image> AddImageAsync(IFormFile image, int editId) {
      // Save the image to wherever we are storing it.. for now just the file system.
      var fileType = this.MimeTypeMap[image.ContentType];
      
      string imageName = await _fileRepository.SaveAsync(fileType, 
        async (stream) => await _imageProcessor.ProcessImageToStream(image, stream));

      string base64Preview = null;
      string thumbnailName = await _fileRepository.SaveAsync(fileType, 
        async (stream) => {
          base64Preview = await _imageProcessor.ProcessThumbnailImageToStream(image, stream);
        }
      );

      // Create an image record in the database.
      var newImage = new Image() {
        Url = $"/api/images/{imageName}",
        ThumbnailUrl = $"/api/images/{thumbnailName}",
        Name = imageName,
        EditId = editId,
        Base64Preview = base64Preview
      };

      _context.Images.Add(newImage);

      _context.SaveChanges();

      return newImage;
    }

    public Stream GetImageStream(string imageName) => _fileRepository.Get(imageName);
  }
}