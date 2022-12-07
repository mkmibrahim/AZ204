using System;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            var result = new WeatherInfoObject();
            // retrieve weatherinfo history
            var weatherhistory = _weatherHistoryManager.GetWeatherInfo(cityName);
            
            
            if (weatherhistory.Count > 0 &&
                (DateTime.Now - weatherhistory.FirstOrDefault().Time).TotalHours < 2)
            {
                result.Time = weatherhistory.FirstOrDefault().Time;
                result.Temperature = weatherhistory.FirstOrDefault().Temperature;
                result.Humidity = weatherhistory.FirstOrDefault().Humidity;
                result.History = weatherhistory;
            }
            else
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

            

                result.Time = time;
                result.Temperature = retrieverInfo.Temperature;
                result.Humidity = retrieverInfo.Humidity;
                result.History = weatherhistory;
            };
            return result;
        }
    }
}
