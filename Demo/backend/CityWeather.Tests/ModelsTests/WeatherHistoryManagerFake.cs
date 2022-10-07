using CityWeather.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWeather.Tests.ModelsTests
{
    internal class WeatherHistoryManagerFake : IWeatherHistoryManager
    {
        List<WeatherInfoInstance> _list = new List<WeatherInfoInstance>();
        public int AddWeatherInfo(WeatherInfoInstance weatherInfoInstance)
        {
            _list.Add(weatherInfoInstance);
            return _list.Count;
        }

        public WeatherInfoInstance GetWeatherInfo(int id)
        {
            throw new NotImplementedException();
        }

        public List<WeatherInfoInstance> GetWeatherInfo(string cityName)
        {
            return _list;
        }
    }
}
