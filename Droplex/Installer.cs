using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Droplex
{
    public static class Installer
    {
        public static async Task Install(string filepath, string args)
        {
            var psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = filepath,
                Arguments = args
            };

            await Task.Run(() =>
            {
                var p = Process.Start(psi);

                while (!p.HasExited)
                    Thread.Sleep(1000);

                if (p.ExitCode != 0)
                    throw new Exception();
            }).ConfigureAwait(false);
        }
    }
}
