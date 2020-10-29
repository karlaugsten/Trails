using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Trails.Controllers
{
    [Route("api/[controller]")]
    public class TrailsController : Controller
    {
        private TrailContext _context;

        public TrailsController(TrailContext context) 
        {
            _context = context;
        }

        [HttpPost("{trailId}/Heart")]
        [Authorize]
        public IActionResult HeartTrail(int trailId)
        {
            var usr = _context.Users
                .Include(u => u.FavouriteTrails)
                .First(u => u.UserName == this.HttpContext.User.Identity.Name);

            var trail = _context.Trails.FirstOrDefault(t => t.TrailId == trailId);
            if(trail == null) return NotFound();

            // If the user already liked the trail, return
            if(usr.FavouriteTrails.Where(fav => fav.TrailId == trailId).Any()) return Ok(successJson());

            usr.FavouriteTrails.Add(new FavouriteTrails() {
                User = usr,
                Trail = trail
            });
            _context.Users.Update(usr);
            _context.SaveChanges();
            return Ok(successJson());
        }

        [HttpPost("{trailId}/Unheart")]
        [Authorize]
        public IActionResult UnHeartTrail(int trailId)
        {
            var usr = _context.Users
                .Include(u => u.FavouriteTrails)
                .First(u => u.UserName == this.HttpContext.User.Identity.Name);

            var trail = _context.Trails.FirstOrDefault(t => t.TrailId == trailId);
            if(trail == null) return NotFound();

            // If the user hasn't already liked the trail, return
            var favourite = usr.FavouriteTrails.Where(fav => fav.TrailId == trailId);
            if(!favourite.Any()) return Ok(successJson());
            
            usr.FavouriteTrails.Remove(favourite.First());
            _context.Users.Update(usr);
            _context.SaveChanges();
            return Ok(successJson());
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<TrailRequest> Trails()
        {
            var trails = _context.Trails
                        .Where(t => t.Approved)
                        .Include(t => t.Edit)
                            .ThenInclude(e => e.Map)
                        .Include(t => t.Edit)
                            .ThenInclude(e => e.Images)
                            .ThenInclude(i => i.Image)
                        .ToList();
            return trails.Select(t => TrailRequest.fromEdit(t.Edit)).ToList();
        }

        [HttpPost]
        [Authorize(Policy = "CanEdit")]
        public IActionResult NewTrail()
        {
            // Creates and returns a new trail with a new edit.
            var newTrail = _context.Trails.Add(new Trail() {
                Title = "",
                Location = "",
                Description = "",
                MaxDuration = 0,
                MinDuration = 0,
                Rating = 0,
                Distance = 0,
                Elevation = 0,
                MaxSeason = "",
                MinSeason = ""
            });
            _context.SaveChanges();
            // Create a new edit for the new trail and return that.
            var newEdit = _context.TrailEdits.Add(new TrailEdit() {
                TrailId = newTrail.Entity.TrailId,
                Title = "",
                Location = "",
                Description = "",
                MaxDuration = 0,
                MinDuration = 0,
                Rating = 0,
                Distance = 0,
                Elevation = 0,
                MaxSeason = "",
                MinSeason = "",
                Images = new List<TrailEditImage>()
            });
            _context.SaveChanges();
            return GetEdit(newEdit.Entity.EditId);
        }

        [HttpPost("{trailId}/Edit")]
        [Authorize(Policy = "CanEdit")]
        public IActionResult Edit(int trailId)
        {
            // Creates and returns a new edit for an existing trail.
            var trail = _context.Trails.FirstOrDefault(t => t.TrailId == trailId);
            if(trail == null) {
                return NotFound();
            }
            var newEdit = _context.TrailEdits.Add(new TrailEdit() {
                TrailId = trail.TrailId,
                Title = trail.Title,
                Location = trail.Location,
                Description = trail.Description,
                MaxDuration = trail.MaxDuration,
                MinDuration = trail.MinDuration,
                Rating = trail.Rating,
                Distance = trail.Distance,
                Elevation = trail.Elevation,
                MaxSeason = trail.MaxSeason,
                MinSeason = trail.MinSeason,
            });
            _context.SaveChanges();
            var edit = newEdit.Entity;
            edit.Images = _context.TrailEditImages.Where(im => im.EditId == trail.EditId).Select(im => new TrailEditImage() {
                    EditId = edit.EditId,
                    ImageId = im.ImageId
                }).ToList();
            _context.TrailEdits.Update(edit);
            _context.SaveChanges();
            return GetEdit(edit.EditId);
        }

        [HttpGet("edit/{editId}")]
        [Authorize(Policy = "CanEdit")]
        public IActionResult GetEdit(int editId)
        {
            // Saves a draft of an edit.
            var trail = _context.TrailEdits
                .Include(t => t.Images)
                    .ThenInclude(i => i.Image)
                .Include(t => t.Map)
                .FirstOrDefault(t => t.EditId == editId);
            if(trail == null)
            {
                return NotFound();
            }
            var response = TrailRequest.fromEdit(trail);
            return Ok(response);
        }

        [HttpPost("{trailId}/Edit/{editId}")]
        [Authorize(Policy = "CanEdit")]
        public IActionResult SaveEdit(int trailId, int editId, [FromBody]TrailRequest edit)
        {
            if(!this.ModelState.IsValid) return BadRequest();
            // Saves a draft of an edit.
            var trail = _context.TrailEdits
                .Include(t => t.Images)
                .FirstOrDefault(t => t.EditId == editId && t.TrailId == trailId);
            if(trail == null)
            {
                return NotFound();
            }
            _context.TrailEdits.Attach(trail);
            //trail.EditId = edit.EditId;
            trail.Title = edit.Title;
            trail.Rating = edit.Rating;
            trail.Location = edit.Location;
            trail.Elevation = edit.Elevation;
            trail.Distance = edit.Distance;
            trail.Description = edit.Description;
            trail.MaxDuration = edit.MaxDuration;
            trail.MinDuration = edit.MinDuration;
            trail.MaxSeason = edit.MaxSeason;
            trail.MinSeason = edit.MinSeason;
            
            _context.TrailEdits.Update(trail);
            // If any of the images on trail dont exist, add them.
            // If any of the images on edit are deleted, delete them.
            var imagesToDelete = trail.Images.Where(im => edit.Images.FirstOrDefault(i => im.ImageId == i.Id) == null);
            imagesToDelete.Select(image => _context.TrailEditImages.Remove(image)).ToList();

            // Save changes in database
            _context.SaveChanges();
            return GetEdit(editId);
        }

        [HttpPost("{trailId}/Commit/{editId}")]
        [Authorize(Policy = "CanCommit")]
        public IActionResult CommitEdit(int trailId, int editId)
        {
            // Commits an edit as the actual trail definition.
            var edit = _context.TrailEdits.FirstOrDefault(t => t.TrailId == trailId && t.EditId == editId);
            if(edit == null)
            {
                return NotFound();
            }
            var trail = _context.Trails.FirstOrDefault(t => t.TrailId == trailId);
            if(trail == null)
            {
                return NotFound();
            }

            // Copy all fields from edit to trail.
            trail.Title = edit.Title;
            trail.Rating = edit.Rating;
            //trail.Images = edit.Images;
            trail.Location = edit.Location;
            trail.Elevation = edit.Elevation;
            trail.Distance = edit.Distance;
            trail.Description = edit.Description;
            trail.MaxDuration = edit.MaxDuration;
            trail.MinDuration = edit.MinDuration;
            trail.MaxSeason = edit.MaxSeason;
            trail.MinSeason = edit.MinSeason;
            trail.EditId = edit.EditId; // Update the EditId to reflect the new edit.

            // For now any time a trail is committed, lets approve it.
            // In the future, there may be an approval process.
            trail.Approved = true;
            _context.Trails.Update(trail);

            // Save changes in database
            _context.SaveChanges();
            return Ok(successJson());
        }

        /// <summary>
        /// Return some json response on success?
        /// </summary>
        /// <returns></returns>
        private static object successJson() {
            return new {
                message = "success"
            };
        }
    }
}
