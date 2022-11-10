using Frontend_Cities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Frontend_Cities.Tests.Helpers
{
    static class optionsHelper
    {
        public static IOptions<ConfigurationClass> CreateOptions()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConfigurationClass configurationClass = new ConfigurationClass()
            {
                //backendUrl = "https://az204demobackendapp123.azurewebsites.net/api/City/"
                backendUrl = "https://localhost:5011/api/City/"
            };
            var options = Options.Create(configurationClass);

            return options;
        }
    }
}
