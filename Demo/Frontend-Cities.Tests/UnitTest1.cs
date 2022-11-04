using Frontend_Cities.Models;
using Frontend_Cities.Tests.Helpers;
using System.Net.Http.Headers;

namespace Frontend_Cities.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void passingTest()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task GetCitiesTestAsync()
        {
            // Arrange
            var cityModel = new CityModel(optionsHelper.CreateOptions(), new HttpClient());

            // Act
            var result = await cityModel.getCitiesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 0, "No cities are returned");
            foreach(var city in result)
            {
                Assert.True(city.Id > -1);
                Assert.NotEmpty(city.Name);
                Assert.NotEmpty(city.Image);
            }
        }
    }
}