using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Droplex
{
    public static class DroplexPackage
    {
        /// <summary>
        /// Core function to download and install the specified app
        /// </summary>
        /// <exception cref="HttpRequestException">Thrown when access to the download url has errored out </exception>
        /// <exception cref="ArgumentException">Thrown when the download url does not correctly reference a downloadable file </exception>
        /// <exception cref="DirectoryNotFound">Thrown when unable to manage the deletion/creaction of download directory </exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when unable to access the download directory location </exception>
        /// <exception cref="InvalidOperationException">Thrown when the install is unable to run correctly </exception>
        /// <exception cref="FileNotFoundException">Thrown when unable to manage the deletion/creaction of download directory </exception>
        /// <exception cref="OperationCanceledException">Thrown when the installation is cancelled or unsuccessful </exception>
        public static async Task Drop(App app)
        {
            var dropList = new DropList();

            var item = dropList.Get(app);

            var directoryPath = Path.Combine(Path.GetTempPath(), "Droplex");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, app.ToString());

            Task downloading = Downloader.Get(item.Url, filePath);

            var downloadedFilePath = $"{filePath}{Path.GetExtension(item.Url)}";

            await downloading.ConfigureAwait(false);

            await Installer.Install(downloadedFilePath, item.Args).ConfigureAwait(false);
        }
    }
}
