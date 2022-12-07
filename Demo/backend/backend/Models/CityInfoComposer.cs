using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Options;
using AutoMapper;

namespace backend.Models
{
    public class CityInfoComposer : ICityInfoComposer
    {
        private readonly ConfigurationClass _configClass;
        private readonly IImageRetriever _imageRetriever;
        private readonly IWeatherRetriever _weatherRetriever;

        public CityInfoComposer(IOptions<ConfigurationClass> options, 
            IImageRetriever imageRetriever, IWeatherRetriever weatherRetriever)
        {
            _configClass = options.Value;
            _imageRetriever = imageRetriever;
            _weatherRetriever = weatherRetriever;
        }

        public async Task<CityInfo> GetInfo(string cityName)
        {
            var rng = new Random();
            

            var retrievedWeatherInfo = await _weatherRetriever.GetWeather(cityName);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<WeatherDTO, WeatherInfo>();
            });
            var mapper = config.CreateMapper();
            var weatherInfo = mapper.Map<WeatherInfo>(retrievedWeatherInfo);

            List<string> images = await _imageRetriever.getImageAsync(cityName,5);
            string imageString;
            if ((DateTime.Now - weatherInfo.Time).TotalHours < 24)
                imageString = await getNewImage(cityName);
            else
                imageString = images.FirstOrDefault();
            
            var result = new CityInfo
            {
                Name = cityName,
                Slug = cityName,
                Image = imageString,
                Images = images,
                Id = 1,
                Weather = weatherInfo,
                Summary = "This is a nice city"
            };
            return result;
        }

        private async Task<string> getNewImage(string cityName)
        {
            var rng = new Random();
            var randomNumer = rng.Next(29);
            List<string> images = await _imageRetriever.getImageAsync(cityName, randomNumer);

            return images.LastOrDefault();
        }
    }
}
