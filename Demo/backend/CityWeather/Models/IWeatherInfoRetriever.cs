using System.Threading.Tasks;

namespace CityWeather.Models
{
    public interface IWeatherInfoRetriever
    {
        public Task<CurrentWeatherObject> RetrieveWeatherInfo(string cityName);
    }

    public class CurrentWeatherObject
    {
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
    }
}
