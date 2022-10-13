using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityInfoComposer _composer;
        private readonly ConfigurationClass _configClass;


        public CityController(ILogger<CityController> logger,
            ICityInfoComposer cityInfoComposer,
            IOptions<ConfigurationClass> options)
        {
            _logger = logger;
            _configClass = options.Value;
            _composer = cityInfoComposer;
        }

        [HttpGet]
        public async Task<CityInfo> GetAsync(string cityName)
        {
            return await _composer.GetInfo(cityName);
        }

        [HttpGet]
        public async Task<List<City>> GetCities()
        {
            var result = new List<City>();
            result.Add(new City
            {
                Id = 1,
                Name = "Amsterdam",
                Image = "https://images.unsplash.com/photo-1542139368-847f8e3fbc6b?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNjU1OTF8MHwxfHNlYXJjaHwxfHxBbXN0ZXJkYW18ZW58MHwxfHx8MTY2NTU3Mjc3Mw&ixlib=rb-1.2.1&q=80&w=1080"
            });
            result.Add(new City
            {
                Id = 2,
                Name = "Paris",
                Image = "https://images.unsplash.com/photo-1522093007474-d86e9bf7ba6f?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNjU1OTF8MHwxfHNlYXJjaHwxfHxQYXJpc3xlbnwwfDF8fHwxNjY1NTYxMTUw&ixlib=rb-1.2.1&q=80&w=1080"
            });
            result.Add(new City
            {
                Id = 1,
                Name = "Cairo",
                Image = "https://images.unsplash.com/photo-1601842995468-2e9a79f3d776?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNjU1OTF8MHwxfHNlYXJjaHwxfHxDYWlyb3xlbnwwfDF8fHwxNjY1NTY5NDI3&ixlib=rb-1.2.1&q=80&w=1080"
            });
            return result;
        }
    }
}
