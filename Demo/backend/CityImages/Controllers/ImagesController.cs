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
        public async Task<List<string>> GetAsync(string cityName, string quantity = "1")
        {
            int.TryParse(quantity, out int intQuantity);
            if (intQuantity == 0)
                throw new ArgumentException("Quantity is not a number");
            var result = await _imageRetriever.RetrieveImages(cityName, intQuantity);
            return result;
        }
    }
}
