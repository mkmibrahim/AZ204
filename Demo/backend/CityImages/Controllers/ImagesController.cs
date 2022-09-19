using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityImages.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> logger;

        public ImagesController(ILogger<ImagesController> logger)
        {
            this.logger = logger;
        }


        [HttpGet("{locationName}")]
        [ActionName("Get")]
        public List<string> Get(string cityName)
        {
            var result = new List<string>();
            result.Add("test1");
            return result;
        }
    }
}
