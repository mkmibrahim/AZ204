using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CityWeather.Tests.IntegrationTests
{
    public class IntegarationTests :IClassFixture<WebApplicationFactory<CityWeather.Startup>>
    {
        private readonly WebApplicationFactory<CityWeather.Startup> _factory;

        public IntegarationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("api/Weather/Get?cityName=Amsterdam")]
        [InlineData("api/Weather/Get?cityName=Paris")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("api/Weather/Get")]
        public async Task GetWithoutcityName_ReturnsError(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
        }
        
    }
}
