using CityWeather.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CityWeather.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherInfoRetriever _weatherInfoRetriever;

        public WeatherController(ILogger<WeatherController> logger, IWeatherInfoRetriever weatherInfoRetriever)
        {
            _logger = logger;
            _weatherInfoRetriever = weatherInfoRetriever;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
                return BadRequest("CityName not provided");
            var response = await _weatherInfoRetriever.RetrieveWeatherInfo(cityName);
            var result = new WeatherInfoResponseMessage()
            {
                temperature = response.Temperature,
                humidity = response.Humidity,
            };
            return Ok(result);
        }
    }

    public class WeatherInfoResponseMessage
    {
        public decimal temperature { get; set; }
        public int humidity { get; set; }
    }
}
