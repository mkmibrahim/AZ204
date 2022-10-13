using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image {get; set; }
    }

    public class CityInfo
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Image {get; set; }
        public int Id { get; set; }
        public WeatherInfo Weather { get; set; }
        public string Summary { get; set; }
        public List<string> Images {get; set; }
    }

    public class WeatherInfo
    {
        public DateTime Time { get; set; }
        public decimal Temperature { get; set; }
        public int Humidity { get; set;}
        public List<WeatherInfo> History { get; set; }
    }
}
