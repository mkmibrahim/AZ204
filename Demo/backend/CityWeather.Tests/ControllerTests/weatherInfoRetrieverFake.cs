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
            result.Time = DateTime.Now;
            result.History = new List<WeatherInfoInstance>();
            result.History.Add(new WeatherInfoInstance()
            {
                Time = DateTime.Now,
                cityName = cityName,
                Temperature = 21.0M,
                Humidity = 40
            });
            return fakeTask;
        }
    }
}
