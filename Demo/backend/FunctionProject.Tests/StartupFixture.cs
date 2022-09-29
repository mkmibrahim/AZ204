using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionProject.Tests
{
    public class StartupFixture :IDisposable
    {
        public StartupFixture()
        {
            var path = AppContext.BaseDirectory;
            var json = File.ReadAllText(Path.Join(path, "local.settings.json"));
            var parsed = JObject.Parse(json).Value<JObject>("Values");

            foreach (var item in parsed)
            {
                Environment.SetEnvironmentVariable(item.Key, item.Value.ToString());
            }

        }

        public void Dispose()
        {

        }

    }
}
