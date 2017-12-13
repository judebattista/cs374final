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
	public class ArtistsModel : PageModel
	{
		private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

		public ArtistsModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
		{
			_context = context;
		}

		public Models.Albums Albums { get; set; }
		public IList<Models.Artists> Artists { get; set; }

		//Note: Id here is an ALBUM ID, not an artists ID. We want to get all the artists on that album
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var artists = (
				from r in _context.Artists
				join f in _context.ArtistFeaturedOnAlbums on r.Id equals f.ArtistId
				join a in _context.Albums on f.AlbumId equals a.Id
				where a.Id == id
				select new Models.Artists
				{
					Id = r.Id,
					Fname = r.Fname,
					Lname = r.Lname,
				});

			Artists = await artists.ToListAsync();

			if (Artists == null)
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