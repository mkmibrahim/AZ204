using backend.Models;
using backend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backend.Tests.ModelsTests
{
    public class CityInfoComposerTests
    {
        [Fact]
        public void CreateCityInfoComposer()
        {
            //Arrange
            var imageRetrieverMock = new Mock<IImageRetriever>();
            var weatherRetrieverMock = new Mock<IWeatherRetriever>();

            //Act
            var composer = new CityInfoComposer(optionsHelper.CreateOptions(), 
                                imageRetrieverMock.Object, weatherRetrieverMock.Object);

            //Assert
            Assert.NotNull(composer);
        }

        [Fact]
        public async void GetInfoTest()
        {
            //Arrange
            var imageRetrieverMock = new Mock<IImageRetriever>();
            var images = new List<string>()
            {
                "string1",
                "string2"
            };
            imageRetrieverMock.Setup(i => i.getImageAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(images));
            var weatherRetrieverMock = new Mock<IWeatherRetriever>();
            var weatherResult = new WeatherDTO
            {
                Temperature = 15,
                Humidity = 30,
                History = new List<WeatherDTO>
                {
                    new WeatherDTO{Temperature = 20, Humidity = 50, Time = new DateTime(2022, 11, 10, 22, 10, 15)},
                    new WeatherDTO{Temperature = 25, Humidity = 55, Time = new DateTime(2022, 11, 11, 21, 15, 15)},
                }
            };
            weatherRetrieverMock.Setup(w => w.GetWeather(It.IsAny<string>()))
                .Returns(Task.FromResult(weatherResult));
            var composer = new CityInfoComposer(optionsHelper.CreateOptions(), 
                                imageRetrieverMock.Object, weatherRetrieverMock.Object);
            var cityName = "Paris";

            //Act
            var result = await composer.GetInfo(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityName, result.Name);
            Assert.Equal(cityName, result.Slug);
            Assert.False(string.IsNullOrEmpty(result.Summary));
            Assert.Equal("string1",result.Image);
            Assert.Equal(2,result.Images.Count);
            Assert.Equal("string1",result.Images[0]);
            Assert.Equal("string2",result.Images[1]);
            Assert.Equal(15, result.Weather.Temperature);
            Assert.Equal(30, result.Weather.Humidity);
        }

        [Fact]
        public async void GetNewImageTest()
        {
            // Arrange
            var imageRetrieverMock = new Mock<IImageRetriever>();
            var cityName = "Paris";
            var images = new List<string>()
            {
                "string1",
                "string2",
                "string3"
            };
            imageRetrieverMock.Setup(i => i.getImageAsync(cityName, It.IsAny<int>()))
                .Returns(Task.FromResult(images));
            var composer = new CityInfoComposer(optionsHelper.CreateOptions(),
                        imageRetrieverMock.Object, new Mock<IWeatherRetriever>().Object);

            // Act
            var result = await composer.GetNewImage(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityName, result.Name);
            Assert.Equal(string.Empty, result.Slug);
            Assert.Contains("string", result.Image);
            Assert.Equal(string.Empty, result.Summary);
        }
    }
}
