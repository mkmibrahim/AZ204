using CityImages.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityImages.Tests.Helpers
{
    static class optionsHelper
    {
        public static IOptions<UnsplashConfigurationClass> CreateOptions() {
            UnsplashConfigurationClass configurationClass = new UnsplashConfigurationClass()
            {
                Client_ID = "123"
            };
            var options = Options.Create(configurationClass);
            return options;
            }
    }
}
