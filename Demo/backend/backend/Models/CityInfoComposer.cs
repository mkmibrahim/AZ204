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

namespace backend.Models
{
    public class CityInfoComposer : ICityInfoComposer
    {
        private IConfiguration _configuration;

        public CityInfoComposer()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json").Build();
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
            //string uri = "http://localhost:7071/api/GetImages?city="+cityName;
            string configValue = _configuration.GetSection("configuration")
                .GetChildren().FirstOrDefault(config => config.Key == "AzureFunctionUrl").Value;
            string uri = configValue + cityName;
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
