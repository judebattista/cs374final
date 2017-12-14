using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesJazz.Models
{
    public partial class Albums
    {
		public Albums()
        {
            AlbumContainsTracks = new HashSet<AlbumContainsTracks>();
            ArtistFeaturedOnAlbums = new HashSet<ArtistFeaturedOnAlbums>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public Int32? YearReleased { get; set; }
        public int? Duration { get; set; }

		public int? DurationM {
			get { return Duration / 60; }
			set { }
		}
		[DisplayFormat(DataFormatString = "{0:00}", ApplyFormatInEditMode = true)]
		public int? DurationS {
			get { return Duration % 60; }
			set { }
		 }

        public ICollection<AlbumContainsTracks> AlbumContainsTracks { get; set; }
        public ICollection<ArtistFeaturedOnAlbums> ArtistFeaturedOnAlbums { get; set; }
    }
}
