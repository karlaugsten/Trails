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

    private Dictionary<string, string> MimeTypeMap = new Dictionary<string, string>() {
      {"image/jpeg", ".jpeg"},
      {"image/png", ".png"},
      {"image/tiff", ".tiff"}
    };

    private string folder;

    public FileImageRepository(TrailContext context, IConfiguration config)
    {
      _context = context;
      folder = config["ImageRepoFolder"];
    }
    
    public async Task<Image> AddImageAsync(IFormFile image, int editId) {
      // Save the image to wherever we are storing it.. for now just the file system.
      var imageName = Guid.NewGuid().ToString() + this.MimeTypeMap[image.ContentType];
      var imagePath = this.folder + imageName;
      using (var stream = new FileStream(imagePath, FileMode.CreateNew))
      {
          await image.CopyToAsync(stream);
      }
      // Create an image record in the database.
      var newImage = new Image() {
        Url = $"/api/images/{imageName}",
        ThumbnailUrl = $"/api/images/{imageName}",
        Name = imageName,
        EditId = editId
      };
      _context.Images.Add(newImage);

      _context.SaveChanges();

      return newImage;
    }

    public Stream GetImageStream(string imageName) 
    {
      var imagePath = this.folder + imageName;
      if(!System.IO.File.Exists(imagePath)) { // TODO: Do the check in imagerepo somehow...
        throw new KeyNotFoundException("No image.");
      }
      FileStream fileStream = new FileStream(imagePath, FileMode.Open);
      return fileStream;      
    }
  }
}