using System;

namespace backend.Models
{
    public class CityInfoComposer : ICityInfoComposer
    {
        public CityInfoComposer()
        {
        }

        public CityInfo GetInfo(string cityName)
        {
            var rng = new Random();
            var result = new CityInfo
            {
                Name = cityName,
                Slug = cityName,
                Image = cityName+".jpg",
                Id = 1,
                Date = DateTime.Now,
                Temperature = rng.Next(-20, 45),
                Humidity = rng.Next(20, 80),
                Summary = "This is a nice city"
            };
            return result;
        }
    }
}
