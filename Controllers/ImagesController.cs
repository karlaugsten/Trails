using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trails.Repositories;

namespace Trails.Controllers
{
    public class ImageUpload {
        public IFormFile File { get; set; }
        public int EditId { get; set; }
    }

    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        public static IEnumerable<string> allowedImages = new List<string>() {
            "image/png",
            "image/jpeg",
            "image/tiff"
        };

        private IImageRepository _imageRepo;

        public ImagesController(IImageRepository imageRepo) {
            _imageRepo = imageRepo;
        }

        [HttpPost]
        public async Task<IActionResult> AddImage(ImageUpload imageUpload)
        {
            if(imageUpload == null || imageUpload.File == null)
            {
                return BadRequest();
            }

            if (!allowedImages.Contains(imageUpload.File.ContentType))
            {
                return BadRequest("File/Image type not allowed");
            }
            // Save the image to the image repository and return a url pointing to the image.
            var newImage = await _imageRepo.AddImageAsync(imageUpload.File, imageUpload.EditId);
            return Ok(newImage);
        }

        [HttpGet("{image}")]
        public IActionResult GetImage(string image)
        {
            try {
                return new FileStreamResult(_imageRepo.GetImageStream(image), "image/jpeg");
            } catch (KeyNotFoundException e) { // Reusing this exception type...
                return NotFound();
            }
        }
    }
}
