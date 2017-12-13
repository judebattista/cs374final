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
	public class AlbumsModel : PageModel
	{
		private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

		public AlbumsModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
		{
			_context = context;
		}

		public Models.Artists Artists { get; set; }
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
				join f in _context.ArtistFeaturedOnAlbums on a.Id equals f.AlbumId
				join r in _context.Artists on f.ArtistId equals r.Id
				where r.Id == id
				select new Models.Albums
				{
					Id = a.Id,
					Title = a.Title,
					Duration = a.Duration,
					DateReleased = a.DateReleased
				});

			Albums = await albums.ToListAsync();

			if (Albums == null)
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