using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;

        //private readonly int _sessionId;
        private readonly ICityInfoComposer _composer;


        public WeatherController(ILogger<WeatherController> logger,
            ICityInfoComposer cityInfoComposer)
        {
            _logger = logger;
            _composer = cityInfoComposer;
        }

        [HttpGet("{locationName}")]
        [ActionName("Get")]
        public CityInfo Get([FromRoute]string locationName)
        {
            return _composer.GetInfo(locationName);
        }
    }
}
