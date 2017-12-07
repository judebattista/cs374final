using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
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
        public DateTime? DateReleased { get; set; }
        public int? Duration { get; set; }

        public ICollection<AlbumContainsTracks> AlbumContainsTracks { get; set; }
        public ICollection<ArtistFeaturedOnAlbums> ArtistFeaturedOnAlbums { get; set; }
    }
}
