using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Models
{
    public interface IImageRetriever
    {
        public Task<List<string>> getImageAsync(string cityName, int quantity = 1);
    }
}
