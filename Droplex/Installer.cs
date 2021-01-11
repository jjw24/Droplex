using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Droplex
{
    public static class Installer
    {
        /// <summary>
        /// Installs from the specified location using the passed in silent install arguements
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the install is unable to run correctly </exception>
        /// <exception cref="FileNotFoundException">Thrown when unable to manage the deletion/creaction of download directory </exception>
        /// <exception cref="OperationCanceledException">Thrown when the installation is cancelled or unsuccessful </exception>
        public static async Task Install(string filepath, string installArgs)
        {
            var psi = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = filepath,
                Arguments = installArgs
            };

            await Task.Run(() =>
            {
                var p = Process.Start(psi);

                while (!p.HasExited)
                    Thread.Sleep(1000);

                if (p.ExitCode != 0)
                    throw new OperationCanceledException();
            }).ConfigureAwait(false);
        }
    }
}
