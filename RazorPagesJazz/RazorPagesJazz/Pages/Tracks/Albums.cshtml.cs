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
	public class AlbumsModel : PageModel
	{
		private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

		public AlbumsModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
		{
			_context = context;
		}

		public Models.Tracks Tracks { get; set; }
		public IList<Models.Albums> Albums { get; set; }

		//Note: Id here is an Artist ID, not an album ID. We want to get all the albums featuring that album
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var albums = (
				from a in _context.Albums
				join c in _context.AlbumContainsTracks on a.Id equals c.AlbumId
				join t in _context.Tracks on c.TrackId equals t.Id
				where t.Id == id
				select a);

			Albums = await albums.ToListAsync();

			if (Albums == null)
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