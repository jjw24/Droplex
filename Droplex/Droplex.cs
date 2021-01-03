using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Droplex
{
    public static class Droplex
    {
        public static void Drop(App app)
        {
            var dropList = new DropList();

            var item = dropList.Get(app);

            var directoryPath = Path.Combine(Path.GetTempPath(), "Droplex");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, app.ToString());

            Downloader.Get(item.Url, filePath).Wait();

            var downloadedFilePath = $"{filePath}{Path.GetExtension(item.Url)}";

            Installer.Install(downloadedFilePath, item.Args);
        }
    }

    public enum App
    {
        python3_9_1 = 1,
        python3_8_7 = 2,
        Everything1_3_4_686 = 3
    }
}
