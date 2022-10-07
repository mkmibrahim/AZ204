using CityWeather.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWeather.Tests.ModelsTests
{
    internal class WeatherHistoryManagerFake : IWeatherHistoryManager
    {
        List<WeatherInfoObject> _list = new List<WeatherInfoObject>();
        public int AddWeatherInfo(WeatherInfoObject weatherInfoInstance)
        {
            _list.Add(weatherInfoInstance);
            return _list.Count;
        }

        public WeatherInfoObject GetWeatherInfo(int id)
        {
            throw new NotImplementedException();
        }

        public List<WeatherInfoObject> GetWeatherInfo(string cityName)
        {
            return _list;
        }
    }
}
