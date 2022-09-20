using CityImages.Controllers;
using CityImages.Models;
using CityImages.Tests.Helpers;
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
            var imageRetrieverMock = new Mock<IImageRetriever>().Object;

            // Act
            var controller = new ImagesController(logger, imageRetrieverMock, optionsHelper.CreateOptions());

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async void GetReturnsCityImage()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<ImagesController>>();
            ILogger<ImagesController> logger = loggerMock.Object;
            var imageRetrieverMock = new Mock<IImageRetriever>().Object;
            var controller = new ImagesController(logger, imageRetrieverMock, optionsHelper.CreateOptions());
            var cityName = "Amsterdam";

            // Act
            var response = controller.GetAsync(cityName);

            // Assert
            Assert.NotNull(response);
        }
    }
}
