using CityWeather.Data;

namespace CityWeather.Models
{
    public class WeatherHistoryManager : IWeatherHistoryManager
    {
        private readonly WeatherDbContext _weatherDbContext;



        public void AddWeatherInfo(WeatherInfoInstance weatherInfoInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}
