using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        [HttpGet("{locationName}")]
        [ActionName("Get")]
        public async Task<CityInfo> GetAsync([FromRoute]string locationName)
        {
            return await _composer.GetInfo(locationName);
        }
    }
}
