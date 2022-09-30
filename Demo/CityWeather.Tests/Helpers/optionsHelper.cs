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
                API_Key = "2df118f180dd7070f23a3226cfccae75"
            };
            var options = Options.Create(configurationClass);
            return options;
        }
    }
}
