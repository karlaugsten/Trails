using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trails.Repositories;

namespace Trails.Controllers
{


    [Route("api/[controller]")]
    public class MapsController : Controller
    {
        public class MapUpload {
          public IFormFile File { get; set; }
          public int EditId { get; set; }
        }

        public static IEnumerable<string> allowedMapTypes = new List<string>() {
            "application/gpx+xml",
            "application/octet-stream"
        };

        private IGpxRepository _gpxRepo;

        /// <summary>
        /// This is needed to set properties on the trail.
        /// </summary>
        private TrailContext _trailContext;

        public MapsController(IGpxRepository gpxRepo, TrailContext trailContext) {
            _gpxRepo = gpxRepo;
            _trailContext = trailContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddMap(MapUpload mapUpload)
        {
            if(mapUpload == null || mapUpload.File == null)
            {
                return BadRequest();
            }

            if (!allowedMapTypes.Contains(mapUpload.File.ContentType))
            {
                return BadRequest("Map file format is not supported. Please upload a .gpx map file.");
            }
            // Save the image to the image repository and return a url pointing to the image.
            var map = await _gpxRepo.UploadMap(mapUpload.File.OpenReadStream());
            try {
              // Set the map on this editId to the id of the map returned...
              var edit = _trailContext.TrailEdits.Find(mapUpload.EditId);
              edit.MapId = map.Id;
              edit.Map = map;
              _trailContext.TrailEdits.Update(edit);
              _trailContext.Entry(edit).State = EntityState.Modified;
              _trailContext.SaveChanges();
            } catch(Exception e) {
              return StatusCode(500, "failed to update the edit with the newly created map.");
            }
            return Ok(map);
        }

        [HttpGet("{mapId}")]
        public IActionResult GetMap(int mapId)
        {
            try {
                return Ok(_gpxRepo.GetMap(mapId));
            } catch (KeyNotFoundException e) { // Reusing this exception type...
                return NotFound();
            }
        }

        [HttpGet("/raw/{name}")]
        public IActionResult GetRawMap(string name)
        {
            // TODO: Allow signed in users to download raw maps?
            return Forbid();
        }
    }
}
