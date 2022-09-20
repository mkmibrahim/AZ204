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


        [HttpGet("{cityName}")]
        [ActionName("Get")]
        public async Task<List<string>> GetAsync(string cityName)
        {
            var result = await _imageRetriever.RetrieveImages(cityName);
            return result;
        }
    }
}
