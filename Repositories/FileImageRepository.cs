using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public FileImageRepository(IFileRepository fileRepository, TrailContext context, IConfiguration config, IImageProcessor imageProcessor)
    {
      _context = context;
      _fileRepository = fileRepository;
      _imageProcessor = imageProcessor;
    }
    
    public async Task<Image> AddImageAsync(IFormFile image, int editId) {
      // Save the image to wherever we are storing it.. for now just the file system.
      var fileType = this.MimeTypeMap[image.ContentType];
      string imageUrl = null;
      using(var memStream = new MemoryStream()) {
        await _imageProcessor.ProcessMainImageToStream(image, memStream);
        memStream.Position = 0;
        imageUrl = await _fileRepository.SaveAsync(fileType, memStream);
      }

      // Use a memory stream FOR NOW since it should only be about 200kb. Optimize this later
      using(var memStream = new MemoryStream()) {
        await _imageProcessor.ProcessThumbnailImageToStream(image, memStream);
        memStream.Position = 0;
        string thumbnailUrl = await _fileRepository.SaveAsync(fileType, memStream);
        // Create an image record in the database.
        var newImage = new Image() {
          Url = imageUrl,
          ThumbnailUrl = thumbnailUrl,
          Name = imageUrl.Split("/").Last(),
          EditId = editId,
          Base64Preview = await _imageProcessor.ProcessImageToBase64(image)
        };

        _context.Images.Add(newImage);

        _context.SaveChanges();

        return newImage;
      }
    }

    public Stream GetImageStream(string imageName) => _fileRepository.Get(imageName);
  }
}