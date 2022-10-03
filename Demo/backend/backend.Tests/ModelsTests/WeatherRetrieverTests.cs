using backend.Models;
using backend.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backend.Tests.ModelsTests
{
    public class WeatherRetrieverTests
    {
        [Fact]
        public void CreateWeatherRetriever()
        {
            // Arrange

            // Act
            var retriever = new WeatherRetriever(optionsHelper.CreateOptions());
        }

        [Theory]
        [InlineData("Paris")]
        public async Task RetrieveWeather(string cityName)
        {
            // Arrange 
            var retriever = new WeatherRetriever(optionsHelper.CreateOptions());

            // Act
            var weatherObject = await retriever.GetWeather(cityName);

            // Assert
            Assert.IsType<decimal>(weatherObject.Temperature);
            Assert.IsType<int>(weatherObject.Humidity);
        }
    }
}
