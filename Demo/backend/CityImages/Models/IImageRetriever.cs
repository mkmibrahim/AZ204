using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityImages.Models
{
    public interface IImageRetriever
    {
        public Task<List<string>> RetrieveImages(string locationname, int numberOfImages );
    }
}
