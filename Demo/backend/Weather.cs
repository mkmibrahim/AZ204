using System;

namespace backend
{
    public class Weather
    {
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set;}
        public string Summary { get; set; }
    }
}
