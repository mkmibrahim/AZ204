using CityWeather.Data;
using CityWeather.Models;
using CityWeather.Tests.Helpers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CityWeather.Tests.ModelsTests
{
    public class WeatherHistoryManagerTests
    {
        private WeatherDbContext _context;

        public WeatherHistoryManagerTests()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<WeatherDbContext>().UseSqlite(connection).Options;
            var optionsdb = optionsHelper.CreateOptionsDb();
            _context = new WeatherDbContext(options, optionsdb);

        }

        [Fact]
        public void CreateWeatherHistoryManager()
        {
            // Arrange
            

            // Act 
            IWeatherHistoryManager manager = new WeatherHistoryManager(_context);

            // Assert
            Assert.NotNull(manager);
        }

        [Fact]
        public void AddWeatherInfoCity()
        {
            // Arrange
            IWeatherHistoryManager manager = new WeatherHistoryManager(_context);
            var weatherInfoInstance = new WeatherInfoInstance
            {
                Time = DateTime.Now,
                Temperature = 20,
                Humidity = 10
            };

            // Act
            manager.AddWeatherInfo(weatherInfoInstance);

            // Assert
            var managerToCheck = new WeatherHistoryManager(_context);
            //var weatherInfo = managerToCheck.Get

        }

        [Fact]
        public void AddCityTest()
        {

        }
    }
}
