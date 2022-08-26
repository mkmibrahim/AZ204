using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;

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
        public CityInfo Get([FromRoute]string locationName)
        {
            return _composer.GetInfo(locationName);
        }
    }
}
