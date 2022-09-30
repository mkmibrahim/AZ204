using CityWeather.Models;
using CityWeather.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CityWeather.Tests.ModelsTests
{
    public class WeatherInfoRetrieverTests
    {
        private readonly IWeatherInfoRetriever _weatherInfoRetriever;

        public WeatherInfoRetrieverTests()
        {
            var optionsObject = optionsHelper.CreateOptions();
            _weatherInfoRetriever = new WeatherInfoRetriever(optionsObject);
        }

        [Fact]
        public void CreateWeatherInfoRetrieverTest()
        {
            // Arrange

            // Act already done in test class creation

            // Assert
            Assert.NotNull(_weatherInfoRetriever);
        }

        [Fact]
        public async Task RetrieveWeatherInfoAsync()
        {
            // Arrange
            var city = "Paris";

            // Act
            var response = await _weatherInfoRetriever.RetrieveWeatherInfo(city);

            // Assert
            Assert.IsType<decimal>(response.Temperature);
            Assert.IsType<int>(response.Humidity);

        }
    }
}
