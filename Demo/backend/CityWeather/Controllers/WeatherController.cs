using CityWeather.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
                    Temperature = historic.Temperature,
                    Humidity = historic.Humidity
                };
                history.Add(responseMessageHistory);
            }
            var result = new WeatherInfoResponseMessage()
            {
                Time = response.Time,
                Temperature = response.Temperature,
                Humidity = response.Humidity,
                History = history
            };
            return Ok(result);
        }
    }

    public class WeatherInfoResponseMessage
    {
        [JsonProperty(PropertyName = "Time")]
        public DateTime Time { get; set; }
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public List <WeatherInfoResponseMessage> History { get; set; }
    }
}
