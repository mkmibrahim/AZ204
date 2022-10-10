using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Configuration;
using System.IO;

namespace CityWeather.Data
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<cityData> Cities { get; set; }
        public DbSet<weatherData> WeatherData { get; set; }

        public string DbPath { get; }

        public WeatherDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            }

        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
