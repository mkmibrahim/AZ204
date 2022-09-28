using backend.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace backend.Tests.Helpers
{
    static class optionsHelper
    {
        public static IOptions<ConfigurationClass> CreateOptions() {
            ConfigurationClass configurationClass = new ConfigurationClass()
            {
                Url = "https://localhost:5001/api/Images/Get?cityName="
            };
            var options = Options.Create(configurationClass);
            return options;
            }
    }
}
