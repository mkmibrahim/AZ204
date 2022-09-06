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
        private IConfiguration _configuration;
        private readonly ConfigurationClass _configClass;

        public CityInfoComposer(IOptions<ConfigurationClass> options)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json").Build();

            _configClass = options.Value;
        }

        public async Task<CityInfo> GetInfo(string cityName)
        {
            var rng = new Random();
            var imageString = await getImageAsync(cityName);
            var result = new CityInfo
            {
                Name = cityName,
                Slug = cityName,
                Image = imageString,
                Id = 1,
                Date = DateTime.Now,
                Temperature = rng.Next(-20, 45),
                Humidity = rng.Next(20, 80),
                Summary = "This is a nice city"
            };
            return result;
        }

        private async Task<string> getImageAsync(string cityName)
        {
            HttpClient client = new HttpClient();
            string uri = _configClass.Url + cityName;
            string responseBody="";

            try	
            {
                responseBody = await client.GetStringAsync(uri);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
            
            var details = JObject.Parse(responseBody);
            var imageUrlString = details.SelectToken("image")?.ToObject<string>();
            var result = imageUrlString;
            
            return result;


        }
    }
}
