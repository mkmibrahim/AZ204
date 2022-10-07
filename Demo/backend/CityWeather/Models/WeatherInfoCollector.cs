using System;
using System.Threading.Tasks;

namespace CityWeather.Models
{
    public class WeatherInfoCollector : IWeatherInfoCollector
    {
        private readonly IWeatherInfoRetriever _weatherInfoRetriever;
        private readonly IWeatherHistoryManager _weatherHistoryManager;

        public WeatherInfoCollector(IWeatherInfoRetriever weatherInfoRetriever, 
            IWeatherHistoryManager weatherHistoryManager)
        {
            _weatherInfoRetriever = weatherInfoRetriever;
            _weatherHistoryManager = weatherHistoryManager;
        }

        public async Task<WeatherInfoObject> GetWeatherInfo(string cityName)
        {
            var time = DateTime.Now;
            var retrieverInfo = await _weatherInfoRetriever.RetrieveWeatherInfo(cityName);

            // Add weatherinfo to history
            var WeatherInfoObject = new WeatherInfoObject
            {
                cityName = cityName,
                Time = time,
                Temperature = retrieverInfo.Temperature,
                Humidity = retrieverInfo.Humidity
            };
            _weatherHistoryManager.AddWeatherInfo(WeatherInfoObject);

            // retrieve weatherinfo history
            var weatherhistory = _weatherHistoryManager.GetWeatherInfo(cityName);

            var result = new WeatherInfoObject()
            {
                Time = time,
                Temperature = retrieverInfo.Temperature,
                Humidity = retrieverInfo.Humidity,
                History = weatherhistory
            };
            return result;
        }
    }
}
