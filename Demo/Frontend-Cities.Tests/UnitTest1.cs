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

        [Fact]
        public async Task GetCityInfo()
        {
            // Arrange
            var cityModel = new CityModel(optionsHelper.CreateOptions(), new HttpClient());
            var cityName= "Amsterdam";

            // Act
            var result = await cityModel.getCityInfo(cityName);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(cityName, result[0].Name);
            Assert.IsType<DateTime>(result[0].Weather.Time);
            Assert.True(result[0].Weather.Temperature > -30);
            Assert.True(result[0].Weather.Humidity > 0);
            Assert.True(result[0].Weather.Humidity < 100);
            Assert.True(result[0].Weather.History.Count > 0);
            for (int i = 0; i < result[0].Weather.History.Count; i++)
            {
                Assert.IsType<DateTime>(result[0].Weather.History[i].Time);
            }
        }
    }
}