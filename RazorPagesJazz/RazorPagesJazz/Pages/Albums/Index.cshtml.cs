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
    public class IndexModel : PageModel
    {
        private readonly RazorPagesJazz.Models.jazzRecordingDatabaseContext _context;

        public IndexModel(RazorPagesJazz.Models.jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        public IList<Models.Albums> Albums { get; set; }
		public IList<Models.Artists> Artists { get; set; }
		public SelectList Durations;
		public string AlbumDuration { get; set; }

        public async Task OnGetAsync(string albumTitle, string artistFName, string artistLname)
        {
			//Create a queryable list of durations
			/*
			var durationQuery = from a in _context.Albums
								orderby a.Duration
								select a.Duration;
			*/
			//Create a queryable collection of albums
			var albums = from a in _context.Albums
						 select a;

			//Filter by title
			if (!String.IsNullOrEmpty(albumTitle)) {
				albums = albums.Where(a => a.Title.Contains(albumTitle));
			}

			if (!String.IsNullOrEmpty(artistFName)) {
				var query =
					(from a in _context.Albums
					 join f in _context.ArtistFeaturedOnAlbums on a.Id equals f.AlbumId
					 join r in _context.Artists on f.ArtistId equals r.Id
					 where r.Fname == artistFName
					 select new Models.Albums{
						 Title = a.Title,
						 Duration = a.Duration,
						 DateReleased = a.DateReleased,
					 });
				albums = query;

			}
			//query based on durations
			/*
			if (!String.IsNullOrEmpty(albumDuration)) {
				int duration = Convert.ToInt32(albumDuration);
				albums = albums.Where(x => x.Duration == duration);
			}
			//Create a list of durations to display on the page
			Durations = new SelectList(await durationQuery.Distinct().ToListAsync());
			*/
			Albums = await albums.ToListAsync();
        }
    }
}
