using System;
using System.Collections.Generic;

namespace backend.Models
{
    public class WeatherDTO
    {
        public DateTime Time {get; set; }
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public List<WeatherDTO> History { get; set; }
    }
}
