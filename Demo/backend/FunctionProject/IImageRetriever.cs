using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionProject
{
    public interface IImageRetriever
    {
        public Task<List<string>> RetrieveImages(string city, int numberOfImages);
    }
}