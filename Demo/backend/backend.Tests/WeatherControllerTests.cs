﻿using backend.Controllers;
using backend.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace backend.Tests
{
    public class WeatherControllerTests
    {
        [Fact]
        public void CreateWeatherController()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CityController>>();
            ILogger<CityController> logger = loggerMock.Object;
            var composerMock = new Mock<ICityInfoComposer>().Object;

            //Act
            var controller = new CityController(logger, composerMock);

            //Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public void GetReturnsCityInfo()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CityController>>();
            ILogger<CityController> logger = loggerMock.Object;
            var composerMock = new Mock<ICityInfoComposer>();
            var cityName = "TestCity";
            var summary = "This is a nice city";
            var expectedReturnMessage = new CityInfo{ Name = cityName,
                                                      Slug = cityName,
                                                      Image = cityName+".jpg",
                                                      Summary = summary};
            composerMock.Setup(c => c.GetInfo(cityName)).Returns(expectedReturnMessage);
            var controller = new CityController(logger, composerMock.Object);

            //Act
            var response = controller.Get(cityName);

            //Assert
            Assert.Equal(cityName, response.Name);
            Assert.Equal(cityName, response.Slug);
            Assert.Equal(cityName+".jpg", response.Image);
            Assert.Equal(summary, response.Summary);
        }
    }
}