using Frontend_Cities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Frontend_Cities.Controllers
{
    public class CityController : Controller
    {
        private readonly CityModel _cityModel;

        public CityController(CityModel cityModel)
        {
            _cityModel = cityModel;
        }

        public async Task<IActionResult> Index(string? name)
        {
            if(name is null)
            {
                List<CityData> cities = await _cityModel.getCitiesAsync();
                return View(cities);
            }
            else
            {
                List<CityData> cities = await _cityModel.getCityInfo(name);
                ViewData["name"]= name;
                return View(cities);
            }

            
        }


    }
}
