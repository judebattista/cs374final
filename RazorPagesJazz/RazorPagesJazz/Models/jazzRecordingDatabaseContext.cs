using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RazorPagesJazz.Models
{
    public partial class jazzRecordingDatabaseContext : DbContext
    {
        public virtual DbSet<AlbumContainsTracks> AlbumContainsTracks { get; set; }
        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<ArtistFeaturedOnAlbums> ArtistFeaturedOnAlbums { get; set; }
        public virtual DbSet<ArtistPerformsTracks> ArtistPerformsTracks { get; set; }
        public virtual DbSet<Artists> Artists { get; set; }
        public virtual DbSet<Instruments> Instruments { get; set; }
        public virtual DbSet<TrackPerformedAtVenue> TrackPerformedAtVenue { get; set; }
        public virtual DbSet<Tracks> Tracks { get; set; }
        public virtual DbSet<Venues> Venues { get; set; }

		public jazzRecordingDatabaseContext(DbContextOptions<jazzRecordingDatabaseContext> options) : base(options)
		{ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlbumContainsTracks>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlbumId).HasColumnName("albumId");

                entity.Property(e => e.TrackId).HasColumnName("trackId");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.AlbumContainsTracks)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumContainsTracks_Albums");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.AlbumContainsTracks)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AlbumContainsTracks_Tracks");
            });

            modelBuilder.Entity<Albums>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateReleased)
                    .HasColumnName("dateReleased")
                    .HasColumnType("date");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ArtistFeaturedOnAlbums>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AlbumId).HasColumnName("albumId");

                entity.Property(e => e.ArtistId).HasColumnName("artistId");

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.ArtistFeaturedOnAlbums)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtistFeaturedOnAlbums_Albums");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistFeaturedOnAlbums)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtistFeaturedOnAlbums_Artists");
            });

            modelBuilder.Entity<ArtistPerformsTracks>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArtistId).HasColumnName("artistId");

                entity.Property(e => e.TrackId).HasColumnName("trackId");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistPerformsTracks)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtistPerformsTracks_Artists");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.ArtistPerformsTracks)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtistPerformsTracks_Tracks");
            });

            modelBuilder.Entity<Artists>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Fname)
                    .HasColumnName("fname")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lname)
                    .HasColumnName("lname")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Instruments>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Qualifier)
                    .HasColumnName("qualifier")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TrackPerformedAtVenue>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TrackId).HasColumnName("trackId");

                entity.Property(e => e.VenueId).HasColumnName("venueId");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.TrackPerformedAtVenue)
                    .HasForeignKey(d => d.TrackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrackPerformedAtVenue_Tracks");

                entity.HasOne(d => d.Venue)
                    .WithMany(p => p.TrackPerformedAtVenue)
                    .HasForeignKey(d => d.VenueId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrackPerformedAtVenue_Venues");
            });

            modelBuilder.Entity<Tracks>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateRecorded)
                    .HasColumnName("dateRecorded")
                    .HasColumnType("date");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Venues>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });
        }
    }
}
