using CityWeather.Data;
using CityWeather.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace CityWeather.Tests.Helpers
{
    static class optionsHelper
    {
        public static IOptions<OpenWeatherConfigurationClass> CreateOptions()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            OpenWeatherConfigurationClass configurationClass = new OpenWeatherConfigurationClass()
            {
                API_Key = configuration.GetSection("OpenWeather").GetValue<string>("API_Key")
            };
            var options = Options.Create(configurationClass);
            
            return options;
        }

        public static IOptions<DatabaseConfigurationClass> CreateOptionsDb()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DatabaseConfigurationClass configurationClass = new DatabaseConfigurationClass()
            {
                ConnectionPath = Directory.GetCurrentDirectory(),
                DefaultConnection = configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection")
            };
            var options = Options.Create(configurationClass);
            
            return options;
        }
    }
}
