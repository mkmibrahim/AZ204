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
using System.Collections.Generic;

namespace FunctionProject
{
    public static class GetImages
    {
        [FunctionName("GetImages")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Received a request for city "+ req.Query["city"]);

            string city = req.Query["city"];
            int.TryParse(req.Query["quantity"], out var quantity);

            var imageString = await GetAsync(city, quantity);

            var responseMessage = new{city = city, image = imageString};
            //var jsonToReturn = JsonConvert.SerializeObject(responseMessage);

            return new OkObjectResult(responseMessage);
        }

        private static async Task<List<string>> GetAsync(string city, int quantity)
        {
            //var result = new List<string>();
            IImageRetriever imageRetriever = new ImageRetriever();
            quantity = quantity > 0 ? quantity : 1;

            var retrievedImages = await imageRetriever.RetrieveImages(city, quantity);
            //foreach (var image in retrievedImages)
            //    result.A
            return retrievedImages;
        }
    }
}
