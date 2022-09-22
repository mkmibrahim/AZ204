using CityImages.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CityImages.Tests.ModelsTest
{
    public class imageRetrieverFake : IImageRetriever
    {
        Task<List<string>> IImageRetriever.RetrieveImages(string locationname, int numberOfImages)
        {
            var result = new List<string>();
            var fakeTask = Task.FromResult(result);
            
            for(int i = 0; i < numberOfImages; i++)
            { 
                result.Add("imageUrl_"+i);
            }
            return fakeTask;
        }
    }
}
