using System;

namespace Droplex.Test.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var translator = new Translator();
            Installer.Install(translator.Configuration.Releases[0].Url, translator.Configuration.Releases[0].Args);
        }
    }
}
