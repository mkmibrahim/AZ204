using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Weather GetTemperature()
        {
            var rng = new Random();
            var result = new Weather
            {
                Date = DateTime.Now,
                Temperature = rng.Next(-20, 45),
                Humidity = rng.Next(20, 80),
                Summary = "Fine"
            };
            return result;
        }
    }
}
