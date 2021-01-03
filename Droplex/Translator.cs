using Droplex.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Droplex
{
    public class Translator
    {
        public void TranslateConfiguration()
        {
            var input = new StringReader(Document2);

            var deserializer = new DeserializerBuilder()
            .WithNamingConvention(namingConvention: CamelCaseNamingConvention.Instance)
            .Build();

            var config = deserializer.Deserialize<Configuration>(input);
        }
    }
}
