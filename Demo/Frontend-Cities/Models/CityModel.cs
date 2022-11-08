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
            return result;
        }

        private async Task<List<CityData>> getCitiesFromBackend()
        {
            List<CityData> cities = new List<CityData>();
            string uri = _configClass.backendUrl + "GetCities";
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

    
}
