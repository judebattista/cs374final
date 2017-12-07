using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class Artists
    {
        public Artists()
        {
            ArtistFeaturedOnAlbums = new HashSet<ArtistFeaturedOnAlbums>();
            ArtistPerformsTracks = new HashSet<ArtistPerformsTracks>();
        }

        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }

        public ICollection<ArtistFeaturedOnAlbums> ArtistFeaturedOnAlbums { get; set; }
        public ICollection<ArtistPerformsTracks> ArtistPerformsTracks { get; set; }
    }
}
