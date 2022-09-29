using CityWeather.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityWeather.Tests.Helpers
{
    static class optionsHelper
    {
        public static IOptions<OpenWeatherConfigurationClass> CreateOptions()
        {
            OpenWeatherConfigurationClass configurationClass = new OpenWeatherConfigurationClass()
            {
                API_Key = ""
            };
            var options = Options.Create(configurationClass);
            return options;
        }
    }
}
