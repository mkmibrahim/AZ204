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
        public void GetInfoWithValidInput()
        {
            //Arrange
            var composer = new CityInfoComposer();
            var cityName = "TestCity";

            //Act
            var result = composer.GetInfo(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityName, result.Name);
            Assert.Equal(cityName, result.Slug);
            Assert.False(string.IsNullOrEmpty(result.Summary));


        }
    }
}
