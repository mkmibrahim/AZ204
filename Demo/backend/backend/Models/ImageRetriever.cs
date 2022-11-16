using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace backend.Models
{
    public class ImageRetriever : IImageRetriever
    {
        private readonly ConfigurationClass _configClass;
        private readonly HttpClient _httpClient;

        public ImageRetriever(IOptions<ConfigurationClass> options, HttpClient httpClient)
        {
            _configClass = options.Value;
            _httpClient = httpClient;
        }

        public async Task<List<string>> getImageAsync(string cityName, int quantity = 1)
        {
            List<string> images = new List<string>();
            string uri = _configClass.CityImagesUrl + "?" + "cityName=" + cityName + "&quantity=" + quantity;
            string responseBody = "";

            try
            {
                responseBody = await _httpClient.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            var details = JObject.Parse(responseBody);

            for (int i = 0; i < quantity; i++)
            {
                var imageUrlString = details.SelectToken("images")[i]?.ToObject<string>();
                images.Add(imageUrlString);
            }
            return images;
        }
    }
}
