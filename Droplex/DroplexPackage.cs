using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Droplex
{
    public static class DroplexPackage
    {
        /// <summary>
        /// Downloads and installs the specified app
        /// </summary>
        /// <exception cref="HttpRequestException">Thrown when access to the download url has errored out </exception>
        /// <exception cref="ArgumentException">Thrown when the download url does not correctly reference a downloadable file </exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when unable to manage the deletion/creaction of download directory </exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when unable to access the download directory location </exception>
        /// <exception cref="InvalidOperationException">Thrown when the install is unable to run correctly </exception>
        /// <exception cref="FileNotFoundException">Thrown when unable to manage the deletion/creaction of download directory </exception>
        /// <exception cref="OperationCanceledException">Thrown when the installation is cancelled or unsuccessful </exception>
        /// <param name="zipExtractPath"> Path used only for extracting the contents of the zip file to </param >
        /// <param name="app"> The package specified by App.cs  </param >
        public static async Task Drop(App app, string zipExtractPath = "")
        {
            var dropList = new DropList();

            var item = dropList.Get(app);

            var directoryPath = Path.Combine(Path.GetTempPath(), "Droplex");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, app.ToString());

            Task downloading = await Downloader.CheckGoogleConnection() switch
            {
                true => Downloader.Get(item.Url, filePath),
                false => Downloader.Get(item.Mirror ?? item.Url, filePath)
            };

            var downloadedFilePath = $"{filePath}{Path.GetExtension(item.Url)}";

            await downloading.ConfigureAwait(false);

            if (!ZipManager.IsZip(downloadedFilePath))
            {
                await Installer.Install(downloadedFilePath, item.Args).ConfigureAwait(false);
            }
            else
            {
                if (!string.IsNullOrEmpty(zipExtractPath))
                    ZipManager.Extract(downloadedFilePath, zipExtractPath);
            }
        }
    }
}
