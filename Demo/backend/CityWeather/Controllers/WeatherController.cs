using CityWeather.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWeather.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherInfoCollector _weatherInfoCollector;

        public WeatherController(ILogger<WeatherController> logger, IWeatherInfoCollector weatherInfoCollector)
        {
            _logger = logger;
            _weatherInfoCollector = weatherInfoCollector;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
                return BadRequest("CityName not provided");
            var response = await _weatherInfoCollector.GetWeatherInfo(cityName);
            var history = new List<WeatherInfoResponseMessage>();
            foreach(var historic in response.History)
            {
                var responseMessageHistory = new WeatherInfoResponseMessage()
                {
                    Time = historic.Time,
                    temperature = historic.Temperature,
                    humidity = historic.Humidity
                };
                history.Add(responseMessageHistory);
            }
            var result = new WeatherInfoResponseMessage()
            {
                Time = response.Time,
                temperature = response.Temperature,
                humidity = response.Humidity,
                History = history
            };
            return Ok(result);
        }
    }

    public class WeatherInfoResponseMessage
    {
        public DateTime Time { get; set; }
        public decimal temperature { get; set; }
        public int humidity { get; set; }
        public List <WeatherInfoResponseMessage> History { get; set; }
    }
}
