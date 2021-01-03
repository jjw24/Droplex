using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Droplex
{
    public static class Downloader
    {
        private const string UserAgent = @"Mozilla/5.0 (Trident/7.0; rv:11.0) like Gecko";

        internal static async Task Get(string url, string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            var client = new HttpClient(new SocketsHttpHandler(), false);
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

            using var response = await client.GetAsync(url).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await using var fileStream = new FileStream(filePath, FileMode.CreateNew);
                await response.Content.CopyToAsync(fileStream).ConfigureAwait(false);
            }
            else
            {
                throw new HttpRequestException($"Error code <{response.StatusCode}> returned from <{url}>");
            }

            var extension = Path.GetExtension(url);
            var downloadedFile = $"{filePath}{extension}";

            if (File.Exists(downloadedFile))
                File.Delete(downloadedFile);

            File.Move(filePath, downloadedFile);
        }
    }
}
