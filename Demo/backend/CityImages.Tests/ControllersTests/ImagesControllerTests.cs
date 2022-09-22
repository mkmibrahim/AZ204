using CityImages.Controllers;
using CityImages.Models;
using CityImages.Tests.Helpers;
using CityImages.Tests.ModelsTest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static CityImages.Controllers.ImagesController;

namespace CityImages.Tests.ControllersTests
{
    public class ImagesControllerTests
    {
        private readonly IImageRetriever _imageRetriever;
        private readonly ImagesController _imagesController;
        
        public ImagesControllerTests()
        {
            var loggerMock = new Mock<ILogger<ImagesController>>();
            ILogger<ImagesController> logger = loggerMock.Object;
            _imageRetriever = new imageRetrieverFake();
            _imagesController = new ImagesController(logger, _imageRetriever, optionsHelper.CreateOptions());
        }

        [Fact]
        public void CreateImagesControllerTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.NotNull(_imagesController);
        }

        [Fact]
        public async void GetWithCityName_ReturnsOkResult()
        {
            // Arrange
            var cityName = "testCity";

            // Act
            var response = await _imagesController.Get(cityName, "1");

            // Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response as OkObjectResult);
        }

        [Fact]
        public async void GetWithCityName_ReturnsRightItems()
        {
            // Arrange
            var cityName = "testCity";

            // Act
            var response = await _imagesController.Get(cityName, "5") as OkObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.IsType<ResponseMessage>(response.Value);
            Assert.Equal(cityName, ((ResponseMessage)response.Value).city);
            Assert.Equal(5, ((ResponseMessage)response.Value).images.Count);
            for(int i = 0; i < 5; i++)
            {
                Assert.Equal("imageUrl_"+i, ((ResponseMessage)response.Value).images[i]);
            }
            
        }

        [Fact]
        public async void GetWithoutCityReturnsBadRequest()
        {
            // Arrange 

            // Act
            var response = await _imagesController.Get(null, "1");

            // Assert.
            Assert.IsType<BadRequestObjectResult>(response);

        }
    }
}
