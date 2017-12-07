using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class ArtistFeaturedOnAlbums
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }

        public Albums Album { get; set; }
        public Artists Artist { get; set; }
    }
}
