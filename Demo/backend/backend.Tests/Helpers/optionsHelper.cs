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
                Url = "http://localhost:7071/api/GetImages?city="
            };
            var options = Options.Create(configurationClass);
            return options;
            }
    }
}
