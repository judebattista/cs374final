using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Tracks
{
	public class ArtistsModel : PageModel
	{
		private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

		public ArtistsModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
		{
			_context = context;
		}

		public Models.Tracks Tracks { get; set; }
		public IList<Models.Artists> Artists { get; set; }

		//Note: Id here is an ARTIST ID, not an track ID. We want to get all the tracks performed by that artist
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var artists = (
				from r in _context.Artists
				join p in _context.ArtistPerformsTracks on r.Id equals p.ArtistId
				join t in _context.Tracks on p.TrackId equals t.Id
				where t.Id == id
				select r);

			Artists = await artists.ToListAsync();

			if (Artists == null)
			{
				return NotFound();
			}

			Tracks = await _context.Tracks.SingleOrDefaultAsync(m => m.Id == id);

			if (Tracks == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}