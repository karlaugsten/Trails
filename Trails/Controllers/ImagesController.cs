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
            var fileProcessingTask = await _imageRepo.AddImageAsync(imageUpload.File, imageUpload.EditId);
            return Ok(fileProcessingTask);
        }

        [HttpGet("file/{fileId}")]
        public async Task<IActionResult> GetFileTask(int fileId)
        {
            try {
                // Redirect to the actual image URL, this is to support pre-signed download URLs from S3.
                return Ok(await _imageRepo.GetProcessingTask(fileId));
            } catch (KeyNotFoundException e) { // Reusing this exception type...
                return NotFound();
            }
        }

        [HttpGet("{image}")]
        public IActionResult GetImage(string image)
        {
            try {
                // Redirect to the actual image URL, this is to support pre-signed download URLs from S3.
                return Redirect(_imageRepo.GetUrl(image));
            } catch (KeyNotFoundException e) { // Reusing this exception type...
                return NotFound();
            }
        }

        [HttpGet("data/{imageId}")]
        public IActionResult GetImageData(int imageId)
        {
            try {
                // Redirect to the actual image URL, this is to support pre-signed download URLs from S3.
                return Ok(_imageRepo.Get(imageId));
            } catch (KeyNotFoundException e) { // Reusing this exception type...
                return NotFound();
            }
        }

        [HttpGet("direct/{image}")]
        public IActionResult GetImageDirect(string image)
        {
            try {
                return new FileStreamResult(_imageRepo.GetImageStream(image), "image/jpeg");
            } catch (KeyNotFoundException e) { // Reusing this exception type...
                return NotFound();
            }
        }
    }
}
