using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace backend.Tests.ModelsTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<backend.Startup>>
    {
        private readonly WebApplicationFactory<backend.Startup> _factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        
        [Trait("Category","Integration")]
        [Theory]
        [InlineData("/api/City/Get?cityName=amsterdam")]
        [InlineData("/api/City/GetCities")]
        public async Task Get_EndpointReturnsSuccess(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            var test = response.Content.ReadAsStringAsync();
            Assert.NotNull(test);
        }
    }
}
