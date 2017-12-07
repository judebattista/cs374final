using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class AlbumContainsTracks
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int TrackId { get; set; }

        public Albums Album { get; set; }
        public Tracks Track { get; set; }
    }
}
