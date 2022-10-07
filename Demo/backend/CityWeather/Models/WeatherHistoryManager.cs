using CityWeather.Data;

namespace CityWeather.Models
{
    public class WeatherHistoryManager : IWeatherHistoryManager
    {
        private readonly WeatherDbContext _weatherDbContext;

        public WeatherHistoryManager(WeatherDbContext weatherDbContext)
        {
            _weatherDbContext = weatherDbContext;
        }

        public void AddWeatherInfo(WeatherInfoInstance weatherInfoInstance)
        {
            throw new System.NotImplementedException();
        }
    }
}
