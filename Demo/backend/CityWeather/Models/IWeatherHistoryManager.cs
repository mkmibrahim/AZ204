using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;

namespace CityWeather.Models
{
    public interface IWeatherHistoryManager
    {
        public int AddWeatherInfo (WeatherInfoInstance weatherInfoInstance);

        public WeatherInfoInstance GetWeatherInfo(int id);

        public List<WeatherInfoInstance> GetWeatherInfo(string cityName);

    }
}