using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace CityWeather.Models
{
    public interface IWeatherHistoryManager
    {
        public void AddWeatherInfo (WeatherInfoInstance weatherInfoInstance);

    }
}