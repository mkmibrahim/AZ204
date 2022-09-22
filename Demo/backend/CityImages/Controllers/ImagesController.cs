using CityImages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityImages.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly IImageRetriever _imageRetriever;
        private readonly UnsplashConfigurationClass _unsplashConfigurationClass;

        public ImagesController(ILogger<ImagesController> logger, IImageRetriever imageRetriever, 
            IOptions<UnsplashConfigurationClass> options)
        {
            _logger = logger;
            _imageRetriever = imageRetriever;
            _unsplashConfigurationClass = options.Value;
        }


        [HttpGet]
        public async Task<IActionResult> Get(string cityName, string quantity = "1")
        {
            if (string.IsNullOrWhiteSpace(cityName))
                return BadRequest("CityName not provided");
            int.TryParse(quantity, out int intQuantity);
            if (intQuantity == 0)
                return BadRequest("Quantity is not a number");
            var imageString = await _imageRetriever.RetrieveImages(cityName, intQuantity);
            ResponseMessage responseMessage = new ResponseMessage() {
                                                    city = cityName, 
                                                    images = imageString};
            return Ok(responseMessage);
        }

        public class ResponseMessage
        {
            public string city { get; set; }
            public List<string> images { get; set; }
        }
    }
}
