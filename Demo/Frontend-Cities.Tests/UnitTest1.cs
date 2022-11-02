using Frontend_Cities.Models;
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
        public void GetCitiesTest()
        {
            // Arrange
            var cityModel = new CityModel();

            // Act
            var result = cityModel.getCities();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 0, "No cities are returned");
            foreach(var city in result)
            {
                Assert.True(city.Id > 0);
                Assert.NotEmpty(city.Name);
                Assert.NotEmpty(city.Image);
            }
        }
    }
}