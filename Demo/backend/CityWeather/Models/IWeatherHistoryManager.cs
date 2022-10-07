using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Collections.Generic;

namespace CityWeather.Models
{
    public interface IWeatherHistoryManager
    {
        public int AddWeatherInfo (WeatherInfoObject WeatherInfoObject);

        public WeatherInfoObject GetWeatherInfo(int id);

        public List<WeatherInfoObject> GetWeatherInfo(string cityName);

    }
}