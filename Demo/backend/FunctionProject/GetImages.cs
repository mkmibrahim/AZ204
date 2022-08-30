using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;

namespace FunctionProject
{
    public static class GetImages
    {
        [FunctionName("GetImages")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string city = req.Query["city"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            city = city ?? data?.name;

            var imageString = await GetAsync(city);


            string responseMessage = string.IsNullOrEmpty(city)
                ? "This HTTP triggered function executed successfully. Pass a city name in the query string or in the request body for a personalized response."
                : $"Hello, {city}. This HTTP triggered function executed successfully. The image string is "+ imageString;

            return new OkObjectResult(responseMessage);
        }

        private static async Task<string> GetAsync(string city)
        {

            HttpClient client = new HttpClient();
            string uri = "https://api.unsplash.com/search/photos?query=" + city + "&per_page=30&orientation=portrait&page=1&client_id="+Environment.GetEnvironmentVariable("Unsplash_Client_ID");
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
            var imageUrl = details["results"][1]["urls"]["regular"];
            var imageUrlString = details.SelectToken("results[1].urls.regular")?.ToObject<string>();

            return imageUrlString;
        }
    }
}
