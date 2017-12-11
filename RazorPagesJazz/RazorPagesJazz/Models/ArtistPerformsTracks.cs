using System;
using System.Collections.Generic;

namespace RazorPagesJazz.Models
{
    public partial class ArtistPerformsTracks
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public int TrackId { get; set; }

        public Artists Artist { get; set; }
        public Tracks Track { get; set; }
    }
}
