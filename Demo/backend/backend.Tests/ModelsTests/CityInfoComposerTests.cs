using backend.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace backend.Tests.ModelsTests
{
    public class CityInfoComposerTests
    {
        [Fact]
        public void CreateCityInfoComposer()
        {
            //Arrange


            //Act
            var composer = new CityInfoComposer();

            //Assert
            Assert.NotNull(composer);
        }

        [Fact]
        public async void GetInfoWithValidInput()
        {
            //Arrange
            var composer = new CityInfoComposer();
            var cityName = "Paris";

            //Act
            var result = await composer.GetInfo(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityName, result.Name);
            Assert.Equal(cityName, result.Slug);
            Assert.False(string.IsNullOrEmpty(result.Summary));


        }
    }
}
