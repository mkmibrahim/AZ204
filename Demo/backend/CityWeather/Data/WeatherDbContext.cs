using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace CityWeather.Data
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<cityData> Cities { get; set; }
        public DbSet<weatherData> WeatherData { get; set; }

        public string DbPath { get; }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options, IOptions<DatabaseConfigurationClass> options2) : base (options)
        {
            string folder = Directory.GetCurrentDirectory();
            DbPath = Path.Join(folder, options2.Value.DefaultConnection);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source = {DbPath}");
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cityData>(entity =>
            { 
                entity.HasKey(e => e.cityId);
                entity.Property(e => e.cityName).HasMaxLength(250);
                entity.Property(e => e.counteryName).HasMaxLength(250);

                entity.HasData( new cityData
                {
                    cityId = 1,
                    cityName = "Amsterdam",
                    counteryName = "Netherlands"
                });

            });

            modelBuilder.Entity<weatherData>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.time);
                entity.Property(e => e.cityId);
                entity.Property(e => e.temperature);
                entity.Property(e => e.humidity);
                
            });
            
            ;
        }
    }
}
