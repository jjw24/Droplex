using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Droplex
{
    public static class Droplex
    {
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

    public enum App
    {
        python3_9_1 = 1,
        python3_8_7 = 2,
        Everything1_3_4_686 = 3
    }
}
