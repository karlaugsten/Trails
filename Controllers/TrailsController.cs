using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public IEnumerable<Trail> Trails()
        {
            return _context.Trails.Where(t => t.Approved).ToList();
        }

        [HttpPost]
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
                Elevation = 0
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
                Elevation = 0
            });
            _context.SaveChanges();
            return Ok(newEdit.Entity);
        }

        [HttpPost("{trailId}/Edit")]
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
                Images = trail.Images // TODO: Will foreign key's be automatically mapped here? Or do we need to do the mapping ourselves.
            });
            _context.SaveChanges();
            return Ok(newEdit.Entity);
        }

        [HttpGet("edit/{editId}")]
        public IActionResult Getdit(int editId)
        {
            // Saves a draft of an edit.
            var trail = _context.TrailEdits.FirstOrDefault(t => t.EditId == editId);
            if(trail == null)
            {
                return NotFound();
            }
            return Ok(trail);
        }

        [HttpPost("{trailId}/Edit/{editId}")]
        public IActionResult SaveEdit(int trailId, int editId, [FromBody]TrailEdit edit)
        {
            // Saves a draft of an edit.
            var trail = _context.TrailEdits.FirstOrDefault(t => t.EditId == editId && t.TrailId == trailId);
            if(trail == null)
            {
                return NotFound();
            }
            trail.Title = edit.Title;
            trail.Rating = edit.Rating;
            trail.Images = edit.Images;
            trail.Location = edit.Location;
            trail.Elevation = edit.Elevation;
            trail.Distance = edit.Distance;
            trail.Description = edit.Description;
            trail.MaxDuration = edit.MaxDuration;
            trail.MinDuration = edit.MinDuration;
            _context.TrailEdits.Update(trail);

            // Save changes in database
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("{trailId}/Commit/{editId}")]
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
            trail.Images = edit.Images;
            trail.Location = edit.Location;
            trail.Elevation = edit.Elevation;
            trail.Distance = edit.Distance;
            trail.Description = edit.Description;
            trail.MaxDuration = edit.MaxDuration;
            trail.MinDuration = edit.MinDuration;
            trail.EditId = edit.EditId; // Update the EditId to reflect the new edit.

            // For now any time a trail is committed, lets approve it.
            // In the future, there may be an approval process.
            trail.Approved = true;
            _context.Trails.Update(trail);

            // Save changes in database
            _context.SaveChanges();
            return Ok();
        }
    }
}
