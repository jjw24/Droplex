using System;
using System.Diagnostics;
using System.Threading;

namespace Droplex
{
    public static class Installer
    {
        public static void Install(string filepath, string args)
        {
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = false;
            psi.FileName = filepath;
            psi.Arguments = args;
            var p = Process.Start(psi);

            while (!p.HasExited)
                Thread.Sleep(1000);

            if (p.ExitCode != 0)
                throw new Exception();
        }
    }
}
