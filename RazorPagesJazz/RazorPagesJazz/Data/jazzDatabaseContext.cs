using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

    public class jazzRecordingDatabaseContext : DbContext
    {
        public jazzRecordingDatabaseContext (DbContextOptions<jazzRecordingDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesJazz.Models.Tracks> Tracks { get; set; }

        public DbSet<RazorPagesJazz.Models.Artists> Artists { get; set; }
    }
