using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class Venues
    {
        public Venues()
        {
            TrackPerformedAtVenue = new HashSet<TrackPerformedAtVenue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TrackPerformedAtVenue> TrackPerformedAtVenue { get; set; }
    }
}
