using Frontend_Cities.Controllers;
using Frontend_Cities.Models;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend_Cities.Tests.ControllersTests
{
    public class CityControllerTests
    {
        [Fact]
        public void PassingTest()
        {
            Assert.True(true);
        }

        [Fact]
        public async void GetCitiesTest()
        {
            //Arrange
            var cityModelMock = new Mock<ICityModel>();
            var cityModelReturnMessage = new List<CityData>()
            {
                new CityData
                {
                    Id = 1,
                    Name = "Amsterdam",
                    Image = "string1"
                },
                new CityData
                {
                    Id = 2,
                    Name = "Paris",
                    Image = "string2"
                },
                new CityData
                {
                    Id = 3,
                    Name = "Cairo",
                    Image = "string3"
                }
            };
            
            cityModelMock.Setup( c => c.getCitiesAsync()).ReturnsAsync(cityModelReturnMessage);

            var controller = new CityController(cityModelMock.Object);
            

            // Act
            var result = await controller.Index(null) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            var citiesAvailable = result.Model as List<CityData>;
            Assert.Equal(cityModelReturnMessage, citiesAvailable);
        }

        [Fact]
        public async void GetCityInfoTest()
        {
            // Arrange
            var cityModelMock = new Mock<ICityModel>();
            var cityName = "testCity";
            var cityModelCityInfoReturnMessage = new List<CityData>
            {
                new CityData
                {
                    Id= 1,
                    Name = cityName,
                    Image = cityName + ".jpg",
                    Weather = new WeatherInfo
                    {
                        Time = DateTime.Now,
                        Temperature = (decimal)20.1,
                        Humidity = 35,
                    }
                }
            };
            cityModelMock.Setup(c => c.getCityInfo(cityName)).ReturnsAsync(cityModelCityInfoReturnMessage);
            var controller = new CityController(cityModelMock.Object);

            // Act
            var result = await controller.Index(cityName) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            var citiesAvailable = result.Model as List<CityData>;
            Assert.Equal(cityModelCityInfoReturnMessage, citiesAvailable);
        }

        //[Fact]
        //public async void GetNewImageTest()
        //{
        //    // Arrange
        //    var cityModelMock = new Mock<ICityModel>();
        //    var cityName = "testCity";
        //    var cityModelCityInfoReturnMessage = new List<CityData>
        //    {
        //        new CityData
        //        {
        //            Id= 1,
        //            Name = cityName,
        //            Image = cityName + ".jpg",
        //            Weather = new WeatherInfo
        //            {
        //                Time = DateTime.Now,
        //                Temperature = (decimal)20.1,
        //                Humidity = 35,
        //            }
        //        }
        //    };
        //    cityModelMock.Setup( c => c.getNewImage(cityName)).ReturnsAsync(cityModelCityInfoReturnMessage);
        //    var controller = new CityController(cityModelMock.Object);

        //    // Act
        //    var result = await controller.Index(cityName, "Image") as ViewResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.NotNull(result.Model);
        //    var citiesAvailable = result.Model as List<CityData>;
        //    Assert.Equal(cityModelCityInfoReturnMessage, citiesAvailable);
        //}
    }
}
