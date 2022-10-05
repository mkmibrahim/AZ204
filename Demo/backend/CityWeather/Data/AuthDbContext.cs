using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;

namespace CityWeather.Data
{
    public class AuthDbContext : DbContext
    {
        public DbSet<cityData> Citys { get; set; }

        public string DbPath { get; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "App.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"DataSource= {DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cityData>( entity =>
            {
                entity.HasKey(e => e.cityId);
                entity.Property(e => e.cityName).HasMaxLength(250);
                entity.Property(e => e.counteryName).HasMaxLength(250);

                entity.HasData(new cityData
                {
                    cityId = 1,
                    cityName = "Amsterdam",
                    counteryName = "Netherlands"
                });

            });
        }
    }
}
