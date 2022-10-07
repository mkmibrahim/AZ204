using CityWeather.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CityWeather.Tests.ModelsTests
{
    public class WeatherInfoCollectorTests
    {
        private readonly IWeatherInfoCollector _weatherInfoCollector;

        public WeatherInfoCollectorTests()
        {
            var weatherRetrieverMock =  new Mock<IWeatherInfoRetriever>();
            CurrentWeatherObject mockWeatherInfoObject = new CurrentWeatherObject()
            {
                Temperature = 22,
                Humidity = 15
            };
            weatherRetrieverMock.Setup(w => w.RetrieveWeatherInfo(It.IsAny<string>()))
                .Returns(Task.FromResult(mockWeatherInfoObject));

            var weatherHistoryManagerFace = new WeatherHistoryManagerFake();

            _weatherInfoCollector = new WeatherInfoCollector(weatherRetrieverMock.Object, 
                                            weatherHistoryManagerFace);
        }

        [Fact]
        public void CreatWeatherInfoCollector()
        {
            // Arrange
            
            // Act 

            // Assert
            Assert.NotNull(_weatherInfoCollector);
        }

        [Fact]
        public async Task CollectWeatherInfo()
        {
            // Arrange 
            

            // Act
            var response = await _weatherInfoCollector.GetWeatherInfo("Paris");

            // Assert
            Assert.IsType<DateTime>(response.Time);
            Assert.IsType<decimal>(response.Temperature);
            Assert.IsType<int>(response.Humidity);
            Assert.True(response.Temperature != 0);
            Assert.True(response.Humidity != 0);
            Assert.NotNull(response.History);
            var history = response.History;
            Assert.True(history.Count > 0);
        }
    }
}
