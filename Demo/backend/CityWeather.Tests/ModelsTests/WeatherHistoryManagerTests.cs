using CityWeather.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CityWeather.Tests.ModelsTests
{
    public class WeatherHistoryManagerTests
    {
        [Fact]
        public void CreateWeatherHistoryManager()
        {
            // Arrange
            
            // Act 
            IWeatherHistoryManager manager = new WeatherHistoryManager();

            // Assert
            Assert.NotNull(manager);
        }

        [Fact]
        public void AddWeatherInfo()
        {
            // Arrange
            IWeatherHistoryManager manager = new WeatherHistoryManager();
            var weatherInfoInstance = new WeatherInfoInstance
            {
                Time = DateTime.Now,
                Temperature = 20,
                Humidity = 10
            };

            // Act
            manager.AddWeatherInfo(weatherInfoInstance);
        }
    }
}
