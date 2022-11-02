using Frontend_Cities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend_Cities.Tests.Helpers
{
    static class optionsHelper
    {
        public static IOptions<ConfigurationClass> CreateOptions()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConfigurationClass configurationClass = new ConfigurationClass()
            {
                //backendUrl = configuration.GetSection("ConfigurationSection").GetValue<string>("BackendUrl"),
                backendUrl = "https://az204demobackendapp123.azurewebsites.net/api/City/GetCities"
            };
            var options = Options.Create(configurationClass);

            return options;
        }
    }
}
