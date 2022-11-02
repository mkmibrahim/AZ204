using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using static System.Net.WebRequestMethods;

[assembly:InternalsVisibleTo("Frontend-Cities.Tests")]

namespace Frontend_Cities.Models
{
    public class CityModel
    {
        private readonly ConfigurationClass _configClass;
        private readonly HttpClient _httpClient;

        public CityModel(IOptions<ConfigurationClass> options, HttpClient httpClient)
        {
            _configClass = options.Value;
            _httpClient = httpClient;
        }

        internal async Task<List<CityData>> getCitiesAsync()
        {
            var result = new List<CityData>();

            result = await getCitiesFromBackend();
            //result.Add(new CityData
            //{
            //    Id = 1,
            //    Image = "https://images.unsplash.com/photo-1583295125721-766a0088cd3f?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNTk3Nzl8MHwxfHNlYXJjaHwxfHxhbXN0ZXJkYW18ZW58MHwxfHx8MTY2NzM4NTM5Nw&ixlib=rb-4.0.3&q=80&w=1080",
            //    Name = "Amsterdam"
            //});
            //result.Add(new CityData
            //{
            //    Id = 2,
            //    Image = "https://images.unsplash.com/photo-1511739001486-6bfe10ce785f?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNTk3Nzl8MHwxfHNlYXJjaHwxfHxwYXJpc3xlbnwwfDF8fHwxNjY3Mzg1NDU1&ixlib=rb-4.0.3&q=80&w=1080",
            //    Name = "Paris"
            //});
            //result.Add(new CityData
            //{
            //    Id = 3,
            //    Image = "https://images.unsplash.com/photo-1595979904086-471704dc0e81?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNTk3Nzl8MHwxfHNlYXJjaHwxfHxjYWlyb3xlbnwwfDF8fHwxNjY3Mzg1NDg0&ixlib=rb-4.0.3&q=80&w=1080",
            //    Name = "Cairo"
            //});
            return result;
        }

        private async Task<List<CityData>> getCitiesFromBackend()
        {
            List<CityData> cities = new List<CityData>();
            string uri = "https://az204demobackendapp123.azurewebsites.net/api/City/GetCities";
            //string uri = _configClass.CityImagesUrl + "?" + "cityName=" + cityName + "&quantity=" + quantity;
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

            JArray jsonArray = JArray.Parse(responseBody);
            var details = JObject.Parse(jsonArray[0].ToString());
            

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
    }

    

    public class CityData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class CityDataList
    {
        public List<CityData> cityData;
    }
}
