using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

namespace RazorPagesJazz.Pages.Artists
{
    public class IndexModel : PageModel
    {
        private readonly Models.jazzRecordingDatabaseContext _context;

        public IndexModel(Models.jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        public IList<Models.Artists> Artists { get;set; }

        public async Task OnGetAsync(string albumTitle, string artistFName, string artistLname, string trackName)
        {
			//Get all the artists in the DB. This is not efficient
			var artists = from t in _context.Artists
						 select t;
			//Filter by names
			if (!String.IsNullOrEmpty(artistFName))
			{
				artists = artists.Where(t => t.Fname.Contains(artistFName));
			}

			if (!String.IsNullOrEmpty(artistLname))
			{
				artists = artists.Where(t => t.Lname.Contains(artistLname));
			}

			//Find artists by album title
			if (!String.IsNullOrEmpty(albumTitle))
			{
				var query =
					(from r in artists
					 join f in _context.ArtistFeaturedOnAlbums on r.Id equals f.ArtistId
					 join a in _context.Albums on f.AlbumId equals a.Id
					 where a.Title.Contains(albumTitle)
					 select new Models.Artists
					 {
						 Fname = r.Fname,
						 Lname = r.Lname
					 });
				artists = query;
			}

			//Find artists by track name
			if (!String.IsNullOrEmpty(trackName))
			{
				var query =
					(from r in artists
					 join p in _context.ArtistPerformsTracks on r.Id equals p.ArtistId
					 join t in _context.Tracks on p.TrackId equals t.Id
					 where t.Name.Contains(trackName)
					 select new Models.Artists
					 {
						 Fname = r.Fname,
						 Lname = r.Lname
					 });
				artists = query;
			}
			Artists = await artists.ToListAsync();
		}
    }
}
