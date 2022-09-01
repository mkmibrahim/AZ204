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
using System.Linq;

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

            //string responseMessage = string.IsNullOrEmpty(city)
            //    ? "This HTTP triggered function executed successfully. Pass a city name in the query string or in the request body for a personalized response."
            //    : $"Hello, {city}. This HTTP triggered function executed successfully. The image string is "+ imageString;

            var responseMessage = new{city = city, image = imageString};
            var jsonToReturn = JsonConvert.SerializeObject(responseMessage);

            return new OkObjectResult(responseMessage);
        }

        private static async Task<string> GetAsync(string city)
        {
            var imageRetriever = new ImageRetriever();

            var result = await imageRetriever.RetrieveImages(city);
            return result.FirstOrDefault();
        }
    }
}
