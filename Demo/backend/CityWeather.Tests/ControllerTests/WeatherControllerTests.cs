﻿using CityWeather.Controllers;
using CityWeather.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Xunit;

namespace CityWeather.Tests.ControllerTests
{
    public class WeatherControllerTests
    {
        private readonly WeatherController _weatherController;
        private readonly IWeatherInfoCollector _weatherInfoCollector;

        public WeatherControllerTests()
        {
            var loggerMock = new Mock<ILogger<WeatherController>>();
            ILogger<WeatherController> logger = loggerMock.Object;
            _weatherInfoCollector = new weatherInfoCollectorFake();
            _weatherController = new WeatherController(logger, _weatherInfoCollector);
        }

        [Fact]
        public void CreateWeatherControllerTest()
        {
            // Arrange

            // Act
            

            // Assert
            Assert.NotNull(_weatherController);
        }

        [Fact]
        public async void GetWithCityName_ReturnsOkResult()
        {
            // Arrange 
            var cityName = "testCity";

            // Act
            var response = await _weatherController.Get(cityName);

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response as OkObjectResult);
        }

        [Fact]
        public async void GetWithoutCityReturnsBadRequest()
        {
            // Arrange

            // Act
            var response = await _weatherController.Get(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void GetWithcityName_RetunsWeatherInfo()
        {
            // Arrange
            var cityName = "tesCity";

            // Act
            var response = await _weatherController.Get(cityName);

            // Assert
            Assert.NotNull(response);
            var weatherInfo = response as OkObjectResult;
            var weatherInfoResponse = weatherInfo.Value as WeatherInfoResponseMessage;
            Assert.NotNull(weatherInfoResponse);
            Assert.IsType<decimal>(weatherInfoResponse.Temperature);
            Assert.IsType<int>(weatherInfoResponse.Humidity);
            Assert.Equal(21.0M, weatherInfoResponse.Temperature);
            Assert.Equal(40, weatherInfoResponse.Humidity);
            Assert.Single(weatherInfoResponse.History);
            Assert.Equal(21.0M, weatherInfoResponse.History.First().Temperature);
            Assert.Equal(40, weatherInfoResponse.History.First().Humidity);

        }
    }
}
