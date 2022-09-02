using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FunctionProject
{
    public class ImageRetriever
    {
        public ImageRetriever()
        {

        }

        public async Task<List<string>> RetrieveImages(string city, int numberOfImages = 1)
        {
            var result = new List<string>();
            HttpClient client = new HttpClient();
            string uri = "https://api.unsplash.com/search/photos?query=" + city + "&per_page=30&orientation=portrait&page=1&client_id="+Environment.GetEnvironmentVariable("Client_ID");
            //string uri = "https://api.unsplash.com/search/photos?query=" + city + "&per_page=30&orientation=portrait&page=1&client_id=3ZT5O-ow2-bgTuuwZn7-jf8VS7PoZgTsKW5vj06c2T8";
            string responseBody="";

            try	
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
            
            var details = JObject.Parse(responseBody);
            for(int i = 0; i < numberOfImages; i++)
            {
                var imageUrl = details["results"][1]["urls"]["regular"];
                var imageUrlString = details.SelectToken("results[1].urls.regular")?.ToObject<string>();
                result.Add(imageUrlString);
            }
            return result;
        }
    }
}
