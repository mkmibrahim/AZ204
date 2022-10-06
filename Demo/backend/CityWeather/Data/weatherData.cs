using System;

namespace CityWeather.Data
{
    public class weatherData
    {
        public int Id { get; set; }
        public int cityId { get; set; }
        public DateTime time { get; set; }
        public decimal temperature { get; set; }
        public int humidity { get; set; }
    }
}
