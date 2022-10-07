using CityWeather.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CityWeather.Models
{
    public class WeatherHistoryManager : IWeatherHistoryManager
    {
        private readonly WeatherDbContext _weatherDbContext;

        public WeatherHistoryManager(WeatherDbContext weatherDbContext)
        {
            _weatherDbContext = weatherDbContext;
        }

        public int AddWeatherInfo(WeatherInfoObject weatherInfo)
        {
            int result = 0;
            // check city exists
            var cityrecord = _weatherDbContext.Cities
                                    .Where( c => c.cityName == weatherInfo.cityName )
                                    .ToList()
                                    .FirstOrDefault();
            if (cityrecord == null)
            {
                // Add city
                cityrecord = new cityData()
                {
                    cityName = weatherInfo.cityName
                };
                _weatherDbContext.Cities.Add(cityrecord);
                result = _weatherDbContext.SaveChanges();
            }

            // check weatherinfo exists, if it exists update it otherwise add it
            var weatherdatarecord = _weatherDbContext.WeatherData
                                    .Where(wd => wd.time == weatherInfo.Time &
                                                  wd.cityId == cityrecord.cityId)
                                    .ToList()
                                    .FirstOrDefault();

            if(weatherdatarecord == null)
            {
                weatherdatarecord = new weatherData
                {
                    cityId = cityrecord.cityId,
                    time = weatherInfo.Time,
                    temperature = weatherInfo.Temperature,
                    humidity = weatherInfo.Humidity
                };
                _weatherDbContext.WeatherData.Add(weatherdatarecord);
                _weatherDbContext.SaveChanges();
            }
            else
            {
                weatherdatarecord.cityId = cityrecord.cityId;
                weatherdatarecord.time = weatherInfo.Time;
                weatherdatarecord.temperature = weatherInfo.Temperature;
                weatherdatarecord.humidity = weatherInfo.Humidity;
                _weatherDbContext.SaveChanges();
            }
            result = weatherdatarecord.Id;
            return result;
        }

        public WeatherInfoObject GetWeatherInfo(int id)
        {
            var result = new WeatherInfoObject();
            
            var weatherdatarecord = _weatherDbContext.WeatherData
                                        .Where(wd => wd.Id == id)
                                        .ToList()
                                        .FirstOrDefault();

            cityData cityDataRecord;
            if (weatherdatarecord != null)
            {
                cityDataRecord = _weatherDbContext.Cities
                                    .Where(c => c.cityId == weatherdatarecord.cityId)
                                    .ToList()
                                    .FirstOrDefault();


                result.cityName = cityDataRecord.cityName;
                result.Time = weatherdatarecord.time;
                result.Temperature = weatherdatarecord.temperature;
                result.Humidity = weatherdatarecord.humidity;
                };
            return result;

        }

        public List<WeatherInfoObject> GetWeatherInfo(string cityName)
        {
            var result = new List<WeatherInfoObject>();
            var cityRecord = _weatherDbContext.Cities
                                .Where(c => c.cityName == cityName)
                                .ToList().FirstOrDefault();
            
            if (cityRecord != null)
            {
                var weatherInfoRecords = _weatherDbContext.WeatherData
                                        .Where(wd => wd.cityId == cityRecord.cityId)
                                        .OrderByDescending(wd => wd.time)
                                        .ToList();

                foreach(var weatherInfoRecord in weatherInfoRecords)
                {
                    var newWeatherInfoObject = new WeatherInfoObject
                    {
                        Time = weatherInfoRecord.time,
                        cityName = cityRecord.cityName,
                        Temperature = weatherInfoRecord.temperature,
                        Humidity = weatherInfoRecord.humidity,
                    };
                    result.Add(newWeatherInfoObject);
                }
            }
            
            return result;
        }
    }
}
