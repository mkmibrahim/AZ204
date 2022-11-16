namespace Frontend_Cities.Models
{
    public interface ICityModel
    {
        public Task<List<CityData>> getCitiesAsync();

        public Task<List<CityData>> getCityInfo(string cityName);

    }
}