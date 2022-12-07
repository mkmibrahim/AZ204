using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

[assembly:InternalsVisibleTo("Frontend-Cities.Tests")]

namespace Frontend_Cities.Models
{
    public class CityModel : ICityModel
    {
        private readonly ConfigurationClass _configClass;
        private readonly HttpClient _httpClient;

        public CityModel(IOptions<ConfigurationClass> options, HttpClient httpClient)
        {
            _configClass = options.Value;
            _httpClient = httpClient;
        }

        public async Task<List<CityData>> getCitiesAsync()
        {
            var result = await getInfoFromBackend("GetCities");

            JArray jsonArray = JArray.Parse(result);
            List<CityData> cities = new List<CityData>();
            foreach(JObject item in jsonArray)
            {
                
                var city = new CityData
                {
                    Id = (int)item.GetValue("id"),
                    Name = item.GetValue("name").ToString(),
                    Image = item.GetValue("image").ToString(),
                };
                cities.Add(city);
            }
            return cities;
        }

        public async Task<List<CityData>> getCityInfo(string cityName)
        {
            var postfix = "Get?cityName=" + cityName;
            List<CityData> cities = new List<CityData>();

            var result = await getInfoFromBackend(postfix);
            CityData city = ExtractCityDataFromResponseBackend(result);

            cities.Add(city);
            return cities;
        }

        private CityData ExtractCityDataFromResponseBackend(string result)
        {
            var cityinfodata = JObject.Parse(result);
            var city = new CityData
            {
                Id = (int)cityinfodata.GetValue("id"),
                Name = cityinfodata.GetValue("name").ToString(),
                Image = cityinfodata.GetValue("image").ToString(),
            };

            try
            {
                var weatherToken = cityinfodata.SelectToken("$.weather");
                WeatherInfo weatherInfo = getWeatherInfoFromToken(weatherToken);
                try
                {
                    var historyToken = weatherToken.SelectToken("$.history");
                    var childrenHistoryTokens = historyToken.Children();
                    weatherInfo.History = new List<WeatherInfo>();
                    foreach (var child in childrenHistoryTokens)
                    {
                        WeatherInfo weatherInfoHistory = getWeatherInfoFromToken(child);
                        weatherInfo.History.Add(weatherInfoHistory);
                    }
                }
                catch
                {
                }
                city.Weather = weatherInfo;
            }
            catch (Exception ex)
            {
            }

            return city;
        }

        private WeatherInfo getWeatherInfoFromToken(JToken weatherToken)
        {
            WeatherInfo weatherInfo = new WeatherInfo();
            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.DateFormatString = "yyyy-MM-ddThh:mm:ss:fffz";// "2022-11-09T10:55:26.8363651+00:00"
            weatherInfo.Time = (DateTime)weatherToken.SelectToken("time");
            weatherInfo.Temperature = (decimal)weatherToken.SelectToken("temperature");
            weatherInfo.Humidity = (int)weatherToken.SelectToken("humidity");
            return weatherInfo;
        }

        private async Task<string> getInfoFromBackend(string postfix)
        {
            
            string uri = _configClass.backendUrl + postfix;
            string responseBody = "";

            try
            {
                responseBody = await _httpClient.GetStringAsync(uri);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return responseBody;
        }
 
    }

    

    public struct CityData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public WeatherInfo Weather { get; set; }
    }

    public struct WeatherInfo
    {
        public DateTime Time { get; set; }
        public decimal Temperature { get; set; }
        public int Humidity { get; set;}
        public List<WeatherInfo> History { get; set; }
    }
    
}
