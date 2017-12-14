using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Albums
{
	public class TracksModel : PageModel
	{
		private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

		public TracksModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
		{
			_context = context;
		}

		public Models.Albums Albums { get; set; }
		public IList<Models.Tracks> Tracks { get; set; }

		//Note: Id here is an ALBUM ID, not a track ID. We want to get all the tracks on that album
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var tracks = (
				from t in _context.Tracks
				join at in _context.AlbumContainsTracks on t.Id equals at.TrackId
				join a in _context.Albums on at.AlbumId equals a.Id
				where a.Id == id
				select t);

			Tracks = await tracks.ToListAsync();

			if (Tracks == null)
			{
				return NotFound();
			}

			Albums = await _context.Albums.SingleOrDefaultAsync(m => m.Id == id);

			if (Albums == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}