using System;
using System.Collections.Generic;

namespace RazorPagesJazz.Models
{
    public partial class Tracks
    {
        public Tracks()
        {
            AlbumContainsTracks = new HashSet<AlbumContainsTracks>();
            ArtistPerformsTracks = new HashSet<ArtistPerformsTracks>();
            TrackPerformedAtVenue = new HashSet<TrackPerformedAtVenue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateRecorded { get; set; }
        public int? Duration { get; set; }

        public ICollection<AlbumContainsTracks> AlbumContainsTracks { get; set; }
        public ICollection<ArtistPerformsTracks> ArtistPerformsTracks { get; set; }
        public ICollection<TrackPerformedAtVenue> TrackPerformedAtVenue { get; set; }
    }
}
