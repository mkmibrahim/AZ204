using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CityWeather.Models
{
    public class WeatherInfoRetriever : IWeatherInfoRetriever
    {
        private readonly OpenWeatherConfigurationClass _openWeatherConfigurationClass;

        public WeatherInfoRetriever(IOptions<OpenWeatherConfigurationClass> options)
        {
            _openWeatherConfigurationClass = options.Value;
        }

        public async Task<CurrentWeatherObject> RetrieveWeatherInfo(string cityName)
        {
            var result = new CurrentWeatherObject();
            HttpClient client = new HttpClient();
            string uri = "https://api.openweathermap.org/data/2.5/weather?q="
                + cityName
                + "&APPID="
                + _openWeatherConfigurationClass.API_Key
                + "&units=metric";

            var responseBody = "";

            try
            {
                responseBody = await client.GetStringAsync(uri);
            }
            catch(Exception e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }

            var details = JObject.Parse(responseBody);
            result.Temperature = (decimal)(details.SelectToken("main")?.SelectToken("temp")?.ToObject<decimal>());
            result.Humidity = (int)details.SelectToken("main")?.SelectToken("humidity")?.ToObject<int>();

            return result;
        }
    }
}
