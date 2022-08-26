using System;

namespace backend.Models
{
    public class CityInfo
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Image {get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set;}
        public string Summary { get; set; }
    }
}
