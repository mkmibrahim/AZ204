using System.Threading.Tasks;

namespace backend.Models
{
    public interface IWeatherRetriever
    {
        public Task<WeatherDTO> GetWeather(string cityName);
    }
}
