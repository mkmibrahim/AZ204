﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityWeather.Models
{
    public interface IWeatherInfoCollector
    {
        public Task<WeatherInfoObject> GetWeatherInfo(string cityName);
    }

    public class WeatherInfoObject
    {
        public DateTime Time { get; set; }
        public string cityName { get; set; }
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public List<WeatherInfoObject> History { get; set; }
    }

}