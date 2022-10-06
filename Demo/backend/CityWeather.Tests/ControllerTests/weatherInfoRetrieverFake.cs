using CityWeather.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CityWeather.Tests.ControllerTests
{
    public class weatherInfoCollectorFake : IWeatherInfoCollector
    {
        public weatherInfoCollectorFake()
        {

        }

        public Task<WeatherInfoObject> GetWeatherInfo(string cityName)
        {
            var result = new WeatherInfoObject();
            var fakeTask = Task.FromResult(result);
            result.Temperature = 21.0M;
            result.Humidity = 40;
            return fakeTask;
        }
    }
}
