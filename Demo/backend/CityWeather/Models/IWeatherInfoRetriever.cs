using System.Threading.Tasks;

namespace CityWeather.Models
{
    public interface IWeatherInfoRetriever
    {
        public Task<WeatherInfoObject> RetrieveWeatherInfo(string cityName);
    }

    public class WeatherInfoObject
    {
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
    }
}
