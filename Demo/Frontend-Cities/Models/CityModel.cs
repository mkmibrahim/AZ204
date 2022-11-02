using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("Frontend-Cities.Tests")]

namespace Frontend_Cities.Models
{
    public class CityModel
    {
        internal List<CityData> getCities()
        {
            var result = new List<CityData>();
            result.Add(new CityData
            {
                Id = 1,
                Image = "https://images.unsplash.com/photo-1583295125721-766a0088cd3f?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNTk3Nzl8MHwxfHNlYXJjaHwxfHxhbXN0ZXJkYW18ZW58MHwxfHx8MTY2NzM4NTM5Nw&ixlib=rb-4.0.3&q=80&w=1080",
                Name = "Amsterdam"
            });
            result.Add(new CityData
            {
                Id = 2,
                Image = "https://images.unsplash.com/photo-1511739001486-6bfe10ce785f?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNTk3Nzl8MHwxfHNlYXJjaHwxfHxwYXJpc3xlbnwwfDF8fHwxNjY3Mzg1NDU1&ixlib=rb-4.0.3&q=80&w=1080",
                Name = "Paris"
            });
            result.Add(new CityData
            {
                Id = 3,
                Image = "https://images.unsplash.com/photo-1595979904086-471704dc0e81?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwzNTk3Nzl8MHwxfHNlYXJjaHwxfHxjYWlyb3xlbnwwfDF8fHwxNjY3Mzg1NDg0&ixlib=rb-4.0.3&q=80&w=1080",
                Name = "Cairo"
            });
            return result;
        }
    }

    public class CityData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class CityDataList
    {
        public List<CityData> cityData;
    }
}
