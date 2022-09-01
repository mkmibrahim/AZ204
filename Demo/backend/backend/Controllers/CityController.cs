using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        //private IConfiguration configuration;

        //private readonly int _sessionId;
        private readonly ICityInfoComposer _composer;


        public CityController(ILogger<CityController> logger,
            ICityInfoComposer cityInfoComposer)
        {
            _logger = logger;
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
