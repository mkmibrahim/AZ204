using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class AvailableCitiesComposer : IAvailableCitiesComposer
    {
        private readonly IImageRetriever _imageRetriever;
        private readonly ConfigurationClass _configClass;
        private List<City> _availableCities = new List<City>();

        public AvailableCitiesComposer(IOptions<ConfigurationClass> options,
                                        IImageRetriever imageRetriever)
        {
            _configClass = options.Value;
            _imageRetriever = imageRetriever;
            determineAvailableCities();
        }

        private void determineAvailableCities()
        {
            List<string> citiesList = _configClass.AvailableCities
                                    .Split(',')
                                    .ToList();
            for(int i = 0; i < citiesList.Count; i++)
            {
                var city = new City
                {
                    Id = i,
                    Name = citiesList[i],
                };
                _availableCities.Add(city);
            }
        }

        public async Task<List<City>> GetAvailableCities()
        {
            foreach(City city in _availableCities)
            {
                var images = await _imageRetriever.getImageAsync(city.Name);
                city.Image = images.FirstOrDefault();
            }
            return _availableCities;
        }
    }
}
