﻿using Frontend_Cities.Models;
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

        //public IActionResult Index()
        //{
        //    List<CityData> cities = await _cityModel.getCitiesAsync();
        //    return View(cities);
        //}
        public async Task<IActionResult> Index()
        {
            List<CityData> cities = await _cityModel.getCitiesAsync();
            return View(cities);
        }


    }
}