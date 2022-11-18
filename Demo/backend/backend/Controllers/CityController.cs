using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityInfoComposer _cityInfoComposer;
        private readonly IAvailableCitiesComposer _availableCitiesComposer;



        public CityController(ILogger<CityController> logger,
            ICityInfoComposer cityInfoComposer,
            IAvailableCitiesComposer availableCitiesComposer)
        {
            _logger = logger;
            _cityInfoComposer = cityInfoComposer;
            _availableCitiesComposer = availableCitiesComposer;
        }

        [HttpGet]
        public async Task<CityInfo> GetAsync(string cityName)
        {
            _logger.LogInformation("Info requested for city" + cityName);
            return await _cityInfoComposer.GetInfo(cityName);
        }

        [HttpGet]
        public async Task<List<City>> GetCities()
        {
            _logger.LogInformation("GetCities request recieved.");
            return await _availableCitiesComposer.GetAvailableCities();
        }
    }
}
