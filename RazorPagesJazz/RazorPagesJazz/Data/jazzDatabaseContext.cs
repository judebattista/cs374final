using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesJazz.Models;

    public class jazzDatabaseContext : DbContext
    {
        public jazzDatabaseContext (DbContextOptions<jazzDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesJazz.Models.Tracks> Tracks { get; set; }
    }
