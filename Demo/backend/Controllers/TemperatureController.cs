using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly ILogger<TemperatureController> _logger;

        static readonly Models.ITemperatureInfo temperatureInfo = new Models.TemperatureInfo();

        public TemperatureController(ILogger<TemperatureController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/Temperature")]
        public int GetTemperature()
        {
            return temperatureInfo.GetTemperature();
        }
    }
}
