using CityImages.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CityImages.Tests.ControllersTests
{
    public class ImagesControllerTests
    {
        [Fact]
        public void CreateImagesControllerTest()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ImagesController>>();
            ILogger<ImagesController> logger = loggerMock.Object;

            // Act
            var controller = new ImagesController(logger);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async void GetReturnsCityImage()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ImagesController>>();
            ILogger<ImagesController> logger = loggerMock.Object;
            var controller = new ImagesController(logger);
            var cityName = "Amsterdam";

            // Act
            var response = controller.Get(cityName);

            // Assert
            Assert.NotNull(response);
        }
    }
}
