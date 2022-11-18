using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace backend.Models
{
    public interface ICityInfoComposer
    {
        public Task<CityInfo> GetInfo(string cityName);
    }
}
