using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CityImages.Models
{
    public class ImageRetriever : IImageRetriever
    {
        public async Task<List<string>> RetrieveImages(string locationName, int numberOfImages = 1)
        {
            var result = new List<string>();
            HttpClient client = new HttpClient();
            string uri = "https://api.unsplash.com/search/photos?query=" 
                + locationName 
                + "&per_page=30&orientation=portrait&page=1&client_id="
                +Environment.GetEnvironmentVariable("Client_ID");

            string responseBody="";

            try	
            {
                responseBody = await client.GetStringAsync(uri);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
            
            var details = JObject.Parse(responseBody);
            for(int i = 0; i < numberOfImages; i++)
            {
                var imageUrlString = details.SelectToken("results["+i+"].urls.regular")?.ToObject<string>();
                result.Add(imageUrlString);
            }
            return result;
        }
    }
}
