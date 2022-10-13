using backend.Controllers;
using backend.Models;
using backend.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace backend.Tests
{
    public class CityControllerTests
    {
        [Fact]
        public void CreateCityController()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CityController>>();
            ILogger<CityController> logger = loggerMock.Object;
            var composerMock = new Mock<ICityInfoComposer>().Object;
            var config = new ConfigurationBuilder();

            //Act
            var controller = new CityController(logger, 
                                                new Mock<ICityInfoComposer>().Object,
                                                new Mock<IAvailableCitiesComposer>().Object);

            //Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async void GetReturnsCityInfo()
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
            composerMock.Setup(c => c.GetInfo(cityName)).ReturnsAsync(expectedReturnMessage);

            var controller = new CityController(logger, composerMock.Object, new Mock<IAvailableCitiesComposer>().Object);

            //Act
            var response = await controller.GetAsync(cityName);

            //Assert
            Assert.Equal(cityName, response.Name);
            Assert.Equal(cityName, response.Slug);
            Assert.Equal(cityName+".jpg", response.Image);
            Assert.Equal(summary, response.Summary);
        }

        [Fact]
        public async void GetAvailableCitiesTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<CityController>>();
            ILogger<CityController> logger = loggerMock.Object;
            var availableCitiesComposerMock = new Mock<IAvailableCitiesComposer>();
            var expectedReturnMessage = new List<City>();
            for (int i = 0; i < 5; i++)
            {
                var cityName = "TestCity" + i;
                var city = new City
                {
                    Id = i,
                    Name = cityName,
                    Image = cityName + ".jpg"
                };
                expectedReturnMessage.Add(city);
            }
            
            availableCitiesComposerMock.Setup(c => c.GetAvailableCities()).ReturnsAsync(expectedReturnMessage);

            var controller = new CityController(logger, new Mock<ICityInfoComposer>().Object, availableCitiesComposerMock.Object);

            //Act
            var response = await controller.GetCities();

            //Assert
            Assert.NotNull(response);
            Assert.Equal(5, response.Count);
            for(int i = 0; i < 5; i++)
            {
                Assert.Equal(i, response[i].Id);
                Assert.Equal("TestCity" + i, response[i].Name);
                Assert.Equal("TestCity" + i + ".jpg", response[i].Image);

            }
        }
    }
}
