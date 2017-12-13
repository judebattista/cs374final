using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Artists
{
	public class TracksModel : PageModel
	{
		private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

		public TracksModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
		{
			_context = context;
		}

		public Models.Artists Artists { get; set; }
		public IList<Models.Tracks> Tracks { get; set; }

		//Note: Id here is an ARTIST ID, not a track ID. We want to get all the tracks performed by that artist
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var tracks = (
				from t in _context.Tracks
				join p in _context.ArtistPerformsTracks on t.Id equals p.TrackId
				join r in _context.Artists on p.ArtistId equals r.Id
				where r.Id == id
				select new Models.Tracks
				{
					Id = t.Id,
					Name = t.Name,
					Duration = t.Duration,
					DateRecorded = t.DateRecorded
				});

			Tracks = await tracks.ToListAsync();

			if (Tracks == null)
			{
				return NotFound();
			}

			Artists = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);

			if (Artists == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}