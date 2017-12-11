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
    public class IndexModel : PageModel
    {
        private readonly jazzRecordingDatabaseContext _context;

        public IndexModel(jazzRecordingDatabaseContext context)
        {
            _context = context;
        }

        public IList<Models.Tracks> Tracks { get;set; }

        public async Task OnGetAsync(string albumTitle, string dateRecorded, string artistFName, string artistLname, string trackName)
        {
			//Get all the tracks in the DB. This is not efficient
			var tracks = from t in _context.Tracks
						 select t;
			//Filter by track title
			if (!String.IsNullOrEmpty(trackName)) {
				tracks = tracks.Where(t => t.Name.Contains(trackName));
			}

			//Filter by datereleased
			if (!String.IsNullOrEmpty(dateRecorded))
			{
				tracks = tracks.Where(t => t.DateRecorded.Equals(dateRecorded));
			}


			//Find tracks by album title
			if (!String.IsNullOrEmpty(albumTitle))
			{
				var query =
					(from t in tracks
					 join c in _context.AlbumContainsTracks on t.Id equals c.TrackId
					 join a in _context.Albums on c.AlbumId equals a.Id
					 where a.Title.Contains(albumTitle)
					 select new Models.Tracks
					 {
						 Name = t.Name,
						 Duration = t.Duration,
						 DateRecorded = t.DateRecorded
					 });
				tracks = query;
			}

			//If the user queried on an artist's full name
			if (!String.IsNullOrEmpty(artistFName) && !String.IsNullOrEmpty(artistLname))
			{
				var query =
					(from t in tracks
					 join p in _context.ArtistPerformsTracks on t.Id equals p.TrackId
					 join r in _context.Artists on p.ArtistId equals r.Id
					 where r.Fname == artistFName && r.Lname == artistLname
					 select new Models.Tracks
					 {
						 Name = t.Name,
						 Duration = t.Duration,
						 DateRecorded = t.DateRecorded
					 });
				tracks = query;
			}
			//What if they only used the first name?
			else if (!String.IsNullOrEmpty(artistFName))
			{
				var query =
					(from t in tracks
					 join p in _context.ArtistPerformsTracks on t.Id equals p.TrackId
					 join r in _context.Artists on p.ArtistId equals r.Id
					 where r.Fname == artistFName
					 select new Models.Tracks
					 {
						 Name = t.Name,
						 Duration = t.Duration,
						 DateRecorded = t.DateRecorded
					 });
				tracks = query;
			}
			else if (!String.IsNullOrEmpty(artistLname))
			{
				var query =
					(from t in tracks
					 join p in _context.ArtistPerformsTracks on t.Id equals p.TrackId
					 join r in _context.Artists on p.ArtistId equals r.Id
					 select new Models.Tracks
					 {
						 Name = t.Name,
						 Duration = t.Duration,
						 DateRecorded = t.DateRecorded
					 });
				tracks = query;
			}

			Tracks = await tracks.ToListAsync();
		}
    }
}
