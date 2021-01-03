using Droplex.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Droplex
{
    public class DropList
    {
        public List<Configuration> Configurations;

        public DropList()
        {
            Configurations = TranslateConfigurations();
        }

        public Configuration Get(App app)
        {
            return Configurations.Where(x => x.Id == (int)app).FirstOrDefault();
        }

        private List<Configuration> TranslateConfigurations()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention: CamelCaseNamingConvention.Instance)
                .Build();

            var config = LoadConfigurationFile();

            return deserializer.Deserialize<List<Configuration>>(config);
        }

        private string LoadConfigurationFile()
        {
            try
            {
                using var config = new StreamReader("Configuration.yml");

                return config.ReadToEnd();
            }
            catch(FileNotFoundException e)
            {
                //TODO LOG
                throw e;
            }
        }
    }
}
