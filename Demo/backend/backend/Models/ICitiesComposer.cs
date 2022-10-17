using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Models
{
    public interface IAvailableCitiesComposer
    {
        public Task<List<City>> GetAvailableCities();
    }
}
