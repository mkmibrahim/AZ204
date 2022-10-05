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

        //public CityInfoComposer(IOptions<ConfigurationClass> options)
        //{
        //    _configClass = options.Value;
        //    //_imageRetriever = imageRetriever;
        //    //_weatherRetriever = weatherRetriever;
        //}

        public async Task<CityInfo> GetInfo(string cityName)
        {
            var rng = new Random();
            var imageString = await _imageRetriever.getImageAsync(cityName);
            List<string> images = await _imageRetriever.getImageAsync(cityName,5);
            var weatherInfo = await _weatherRetriever.GetWeather(cityName);
            var result = new CityInfo
            {
                Name = cityName,
                Slug = cityName,
                Image = imageString.FirstOrDefault(),
                Images = images,
                Id = 1,
                Date = DateTime.Now,
                Temperature = weatherInfo.Temperature,
                Humidity = weatherInfo.Humidity,
                Summary = "This is a nice city"
            };
            return result;
        }

        //public async Task<List<string>> getImageAsync(string cityName, int quantity = 1)
        //{
        //    List<string> images = new List<string>();
        //    HttpClient client = new HttpClient();
        //    string uri = _configClass.Url + "?" + "cityName=" + cityName + "&quantity=" + quantity;
        //    string responseBody="";

        //    try	
        //    {
        //        responseBody = await client.GetStringAsync(uri);
        //    }
        //    catch(HttpRequestException e)
        //    {
        //        Console.WriteLine("\nException Caught!");	
        //        Console.WriteLine("Message :{0} ",e.Message);
        //    }
            
        //    var details = JObject.Parse(responseBody);
            
        //    for(int i= 0; i<quantity;i++)
        //    { 
        //        var imageUrlString = details.SelectToken("images")[0]?.ToObject<string>();
        //        images.Add(imageUrlString);
        //    }
        //    return images;
        //}
    }
}
