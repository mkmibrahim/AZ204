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


        [HttpGet("{cityName}/{quantity}")]
        [ActionName("Get")]
        public async Task<ResponseMessage> GetAsync(string cityName, string quantity = "1")
        {
            int.TryParse(quantity, out int intQuantity);
            if (intQuantity == 0)
                throw new ArgumentException("Quantity is not a number");
            var imageString = await _imageRetriever.RetrieveImages(cityName, intQuantity);
            ResponseMessage responseMessage = new ResponseMessage() {
                                                    city = cityName, 
                                                    images = imageString};
            return responseMessage;
        }

        public class ResponseMessage
        {
            public string city { get; set; }
            public List<string> images { get; set; }
        }
    }
}
