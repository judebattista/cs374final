using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public Int32? YearRecorded { get; set; }
        public int? Duration { get; set; }
		public int? DurationM
		{
			get { return Duration / 60; }
			set { }
		}
		[DisplayFormat(DataFormatString = "{0:00}", ApplyFormatInEditMode = true)]
		public int? DurationS
		{
			get { return Duration % 60; }
			set { }
		}
		public ICollection<AlbumContainsTracks> AlbumContainsTracks { get; set; }
        public ICollection<ArtistPerformsTracks> ArtistPerformsTracks { get; set; }
        public ICollection<TrackPerformedAtVenue> TrackPerformedAtVenue { get; set; }
    }
}
