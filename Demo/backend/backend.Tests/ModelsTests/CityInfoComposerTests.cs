using backend.Models;
using backend.Tests.Helpers;
using Microsoft.Extensions.Options;
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
            var composer = new CityInfoComposer(optionsHelper.CreateOptions());

            //Assert
            Assert.NotNull(composer);
        }

        //[Fact(Skip = "Only run when CityImages API is available")]
        [Fact]
        public async void GetInfoWithValidInput()
        {
            //Arrange
            var composer = new CityInfoComposer(optionsHelper.CreateOptions());
            var cityName = "Paris";

            //Act
            var result = await composer.GetInfo(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cityName, result.Name);
            Assert.Equal(cityName, result.Slug);
            Assert.False(string.IsNullOrEmpty(result.Summary));
            for(int i = 0; i < 5; i++)
            {
                Assert.False(string.IsNullOrEmpty(result.Images[i]));
            }


        }
    }
}
