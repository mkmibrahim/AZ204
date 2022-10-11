using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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

            var result = JsonSerializer.Deserialize<WeatherDTO>(responseBody);

            return result;
        }
    }
}
