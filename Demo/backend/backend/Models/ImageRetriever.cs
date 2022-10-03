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

        public ImageRetriever(IOptions<ConfigurationClass> options)
        {
            _configClass = options.Value;
        }

        public async Task<List<string>> getImageAsync(string cityName, int quantity = 1)
        {
            List<string> images = new List<string>();
            HttpClient client = new HttpClient();
            string uri = _configClass.CityImagesUrl + "?" + "cityName=" + cityName + "&quantity=" + quantity;
            string responseBody = "";

            try
            {
                responseBody = await client.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            var details = JObject.Parse(responseBody);

            for (int i = 0; i < quantity; i++)
            {
                var imageUrlString = details.SelectToken("images")[0]?.ToObject<string>();
                images.Add(imageUrlString);
            }
            return images;
        }
    }
}
