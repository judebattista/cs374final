using System;
using System.Collections.Generic;

namespace RazorPagesJazz.Models
{
    public partial class TrackPerformedAtVenue
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public int VenueId { get; set; }

        public Tracks Track { get; set; }
        public Venues Venue { get; set; }
    }
}
