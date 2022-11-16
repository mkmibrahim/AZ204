using Frontend_Cities.Models;
using Frontend_Cities.Tests.Helpers;
using System.Net.Http.Headers;

namespace Frontend_Cities.Tests.ModelsTests
{
    public class CityModelTests
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
            foreach (var city in result)
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
            var cityName = "Amsterdam";

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

        [Fact]
        public async Task GetCityInfoSoonReturnsNewImage()
        {
            // Arrange
            var cityModel = new CityModel(optionsHelper.CreateOptions(), new HttpClient());
            var cityName = "Amsterdam";

            // Act
            var resultFirstCall = await cityModel.getCityInfo(cityName);
            var resultSecondCall = await cityModel.getCityInfo(cityName);

            // Assert
            Assert.Equal(resultFirstCall[0].Name, resultSecondCall[0].Name);
            Assert.Equal(resultFirstCall[0].Weather, resultSecondCall[0].Weather);
            Assert.NotEqual(resultFirstCall[0].Image, resultSecondCall[0].Image);
            
            
        }

        //[Fact]
        //public async Task GetNewImageTest()
        //{
        //    // Arrange
        //    var cityModel = new CityModel(optionsHelper.CreateOptions(), new HttpClient());
        //    var cityName = "Amsterdam";
        //    var resultGetCityInfo = await cityModel.getCityInfo(cityName);
            
        //    // Act
        //    var result = await cityModel.getNewImage(cityName);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Single(result);
        //    Assert.Equal(cityName, result[0].Name);
        //    Assert.NotEqual(resultGetCityInfo.FirstOrDefault().Image, result.FirstOrDefault().Image);
        //    Assert.Equal(resultGetCityInfo.FirstOrDefault().Weather, result.FirstOrDefault().Weather);
        //}
    }
}