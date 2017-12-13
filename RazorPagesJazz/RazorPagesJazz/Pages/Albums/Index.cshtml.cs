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
        private readonly Models.jazzRecordingDatabaseContext _context;

        public IndexModel(Models.jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        public IList<Models.Albums> Albums { get; set; }
		//public SelectList Durations;
		//public string AlbumDuration { get; set; }

        public async Task OnGetAsync(string albumTitle, string dateReleased, string artistFName, string artistLname, string trackName)
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

			//Filter by datereleased
			if (!String.IsNullOrEmpty(dateReleased))
			{
				albums = albums.Where(a => a.DateReleased.Equals(dateReleased));
			}

			//If the user queried on an artist's full name
			if (!String.IsNullOrEmpty(artistFName) && !String.IsNullOrEmpty(artistLname))
			{
				var query =
					(from a in albums
					 join f in _context.ArtistFeaturedOnAlbums on a.Id equals f.AlbumId
					 join r in _context.Artists on f.ArtistId equals r.Id
					 where r.Fname == artistFName && r.Lname == artistLname
					 select new Models.Albums {
						 Title = a.Title,
						 Duration = a.Duration,
						 DateReleased = a.DateReleased,
					 });
				albums = query;
			}
			//What if they only used the first name?
			else if (!String.IsNullOrEmpty(artistFName)) 
			{
				var query =
					(from a in albums
					 join f in _context.ArtistFeaturedOnAlbums on a.Id equals f.AlbumId
					 join r in _context.Artists on f.ArtistId equals r.Id
					 where r.Fname == artistFName
					 select new Models.Albums
					 {
						 Title = a.Title,
						 Duration = a.Duration,
						 DateReleased = a.DateReleased,
					 });
				albums = query;
			}
			else if (!String.IsNullOrEmpty(artistLname))
			{
				var query =
					(from a in albums
					 join f in _context.ArtistFeaturedOnAlbums on a.Id equals f.AlbumId
					 join r in _context.Artists on f.ArtistId equals r.Id
					 where r.Lname == artistLname
					 select new Models.Albums
					 {
						 Title = a.Title,
						 Duration = a.Duration,
						 DateReleased = a.DateReleased,
					 });
				albums = query;
			}

			if (!String.IsNullOrEmpty(trackName))
			{
				var query = 
					(from a in albums
					 join c in _context.AlbumContainsTracks on a.Id equals c.AlbumId
					 join t in _context.Tracks on c.TrackId equals t.Id
					 where t.Name.Contains(trackName)
					 select new Models.Albums
					 {
						 Title = a.Title,
						 Duration = a.Duration,
						 DateReleased = a.DateReleased
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
