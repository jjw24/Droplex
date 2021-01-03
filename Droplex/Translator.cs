using Droplex.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Droplex
{
    public class Translator
    {
        public List<Configuration> Configurations;
        public Translator()
        {
            Configurations = TranslateConfigurations();
        }
        internal List<Configuration> TranslateConfigurations()
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention: CamelCaseNamingConvention.Instance)
                .Build();

            var config = LoadConfigurationFile();

            return deserializer.Deserialize<List<Configuration>>(config);
        }

        internal string LoadConfigurationFile()
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
