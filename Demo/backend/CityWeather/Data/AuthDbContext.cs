using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.Options;

namespace CityWeather.Data
{
    public class AuthDbContext : DbContext
    {
        public DbSet<cityData> Cities { get; set; }

        public string DbPath { get; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options, IOptions<DatabaseConfigurationClass> options2) : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, options2.Value.DefaultConnection );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source= {DbPath}");
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
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

                entity.HasData(new cityData
                {
                    cityId = 2,
                    cityName = "Paris",
                    counteryName = "France"
                });
            });
        }
    }
}
