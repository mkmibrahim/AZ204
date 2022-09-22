using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace CityImages.Tests.ModelsTest
{
    public class BasicIntegrationTests :IClassFixture<WebApplicationFactory<CityImages.Startup>>
    {
        private readonly WebApplicationFactory<CityImages.Startup> _factory;

        public BasicIntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Images/Get?cityName=Amsterdam")]
        [InlineData("/api/Images/Get?cityName=Amsterdam&quantity=5")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/api/Images/get/")]
        [InlineData("/api/Images/Get?cityName=Amsterdam&quantity=invalidInt")]
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
