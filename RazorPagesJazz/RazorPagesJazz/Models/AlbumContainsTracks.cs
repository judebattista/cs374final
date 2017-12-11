using System;
using System.Collections.Generic;

namespace RazorPagesJazz.Models
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
