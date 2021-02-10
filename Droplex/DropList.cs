using Droplex.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Droplex
{
    public class DropList
    {
        private List<Configuration> Configurations;

        internal DropList()
        {
            Configurations = TranslateConfigurations();
        }

        internal Configuration Get(App app)
        {
            return Configurations.Where(x => x.Id == (int)app).FirstOrDefault();
        }

        private List<Configuration> TranslateConfigurations()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention: CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();

            var config = LoadConfigurationFile();

            return deserializer.Deserialize<List<Configuration>>(config);
        }

        private string LoadConfigurationFile()
        {
            try
            {
                using var config = new StreamReader(Directory.GetParent(Assembly.GetExecutingAssembly().Location) + "\\Droplex.Configuration.yml");

                return config.ReadToEnd();
            }
            catch(FileNotFoundException e)
            {
                throw e;
            }
        }
    }
}
