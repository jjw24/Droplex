using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Droplex
{
    public static class ZipManager
    {
        public static bool IsZip(string filePath)
        {
            return string.Equals(Path.GetExtension(filePath), ".zip", StringComparison.OrdinalIgnoreCase);
        }

        public static void Extract(string sourceZipFilePath, string destinationExtractedPath)
        {
            ZipFile.ExtractToDirectory(sourceZipFilePath, destinationExtractedPath, true);
        }
    }
}
