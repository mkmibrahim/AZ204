using System;
using System.Threading.Tasks;

namespace CityWeather.Models
{
    public class WeatherInfoCollector : IWeatherInfoCollector
    {
        private readonly IWeatherInfoRetriever _weatherInfoRetriever;
        private readonly IWeatherHistoryManager _weatherHistoryManager;

        public WeatherInfoCollector(IWeatherInfoRetriever weatherInfoRetriever)
        {
            _weatherInfoRetriever = weatherInfoRetriever;
        }

        public async Task<WeatherInfoObject> GetWeatherInfo(string cityName)
        {
            
            var retrieverInfo = await _weatherInfoRetriever.RetrieveWeatherInfo(cityName);
            
            var result = new WeatherInfoObject()
            {
                Temperature = retrieverInfo.Temperature,
                Humidity = retrieverInfo.Humidity,
                History = new System.Collections.Generic.List<WeatherInfoInstance>()
            };
            return result;
        }
    }
}
