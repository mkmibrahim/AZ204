using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace backend.Models
{
    public class WeatherRetriever : IWeatherRetriever
    {
        private readonly ConfigurationClass _configClass;
        //public Func<HttpClient> ClientFactory= () => new HttpClient();
        private readonly HttpClient _httpClient;

        public WeatherRetriever(IOptions<ConfigurationClass> options, HttpClient httpClient)
        {
            _configClass = options.Value;
            _httpClient = httpClient;
        }

        public async Task<WeatherDTO> GetWeather(string cityName)
        {
            var result = new WeatherDTO();
            //HttpClient client = new HttpClient();
            string uri = _configClass.CityWeatherUrl 
                + "?cityName=" + cityName;
            string responseBody = "";

            try
            {
                responseBody = await _httpClient.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0}", e.Message);
            }

            var details = JObject.Parse(responseBody);
            result.Temperature = (decimal)(details.SelectToken("temperature")?.ToObject<decimal>());
            result.Humidity = (int)(details.SelectToken("humidity")?.ToObject<int>());

            return result;
        }
    }
}
