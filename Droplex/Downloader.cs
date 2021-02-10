using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Droplex
{
    public static class Downloader
    {
        private const string UserAgent = @"Mozilla/5.0 (Trident/7.0; rv:11.0) like Gecko";
        private static readonly HttpClient client = new HttpClient(new SocketsHttpHandler(), false);

        static Downloader()
        {
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            client.Timeout = TimeSpan.FromMinutes(5);
        }

        /// <summary>
        /// Downloads the app from specified file url to the specified file path
        /// </summary>
        /// <exception cref="HttpRequestException">Thrown when access to the download url has errored out </exception>
        /// <exception cref="ArgumentException">Thrown when the download url does not correctly reference a downloadable file </exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when unable to manage the deletion/creaction of download directory </exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when unable to access the download directory location </exception>
        public static async Task Get(string url, string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            // filePath could be passed in with or without extension
            var filePathWithoutExtension =
                Path.Combine(Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));

            // Use ResponseHeadersRead to allow directly copy to stream
            using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await using var fileStream = new FileStream(filePathWithoutExtension, FileMode.CreateNew);
                await response.Content.CopyToAsync(fileStream).ConfigureAwait(false);
            }
            else
            {
                throw new HttpRequestException($"Error code <{response.StatusCode}> returned from <{url}>");
            }

            var extension = Path.GetExtension(url);
            var downloadedFile = $"{filePathWithoutExtension}{extension}";

            if (File.Exists(downloadedFile))
                File.Delete(downloadedFile);

            File.Move(filePathWithoutExtension, downloadedFile);
        }

        public static async Task<bool> CheckGoogleConnection()
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(2000);
                await client.GetAsync("http://clients3.google.com/generate_204", cts.Token);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
