using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Droplex
{
    public static class Droplex
    {
        /// <summary>
        /// Downloads and installs the specified app
        /// </summary>
        /// <exception cref="HttpRequestException">Thrown when access to the download url has errored out </exception>
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
