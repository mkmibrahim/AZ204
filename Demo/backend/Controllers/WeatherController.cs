using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace backend.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    //public class WeatherController : ControllerBase
    //{
    //    private readonly ILogger<WeatherController> _logger;

    //    public WeatherController(ILogger<WeatherController> logger)
    //    {
    //        _logger = logger;
    //    }

    //    [HttpGet]
    //    public Weather GetTemperature()
    //    {
    //        var rng = new Random();
    //        var result = new Weather
    //        {
    //            Name = "Brazil",
    //            Slug = "brazil",
    //            Image = "brazil.jpg",
    //            Id = 1,
    //            Date = DateTime.Now,
    //            Temperature = rng.Next(-20, 45),
    //            Humidity = rng.Next(20, 80),
    //            Summary = "Fine"
    //        };
    //        return result;
    //    }
    //}
    [Route("api/[controller]/[action]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(ILogger<WeatherController> logger)
        {
            _logger= logger;
        }

        [HttpGet("{locationName}")]
        [ActionName("Get")]
        public Weather Get([FromRoute]string locationName)
        {
            var rng = new Random();
            var result = new Weather
            {
                Name = locationName,
                Slug = locationName,
                Image = locationName+".jpg",
                Id = 1,
                Date = DateTime.Now,
                Temperature = rng.Next(-20, 45),
                Humidity = rng.Next(20, 80),
                Summary = "Fine"
            };
            return result;
        }
    }
}
