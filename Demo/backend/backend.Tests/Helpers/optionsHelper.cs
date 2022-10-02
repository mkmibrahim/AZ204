using backend.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Tests.Helpers
{
    static class optionsHelper
    {
        public static IOptions<ConfigurationClass> CreateOptions() {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();
            ConfigurationClass configurationClass = new ConfigurationClass()
            {
                CityImagesUrl = configuration.GetSection("ConfigurationUrls").GetValue<string>("CityImagesUrl"),
                CityWeatherUrl = configuration.GetSection("ConfigurationUrls").GetValue<string>("CityWeatherUrl")
            };
            var options = Options.Create(configurationClass);
            
            return options;
        }
    }
}
