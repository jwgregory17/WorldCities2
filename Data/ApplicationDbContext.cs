﻿using Microsoft.EntityFrameworkCore;
using WorldCities2.Data.Models;

namespace WorldCities2.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor
        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        #endregion Constructor

        #region Methods
        protected override void OnModelCreating(ModelBuilder
            modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map Entity names to DB Table names
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Country>().ToTable("Countries");
        }
        #endregion Methods

        #region Properties
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        #endregion Properties
    }
}
