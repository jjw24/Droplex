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
        
        /// <summary>
        /// Downloads the app from specified file url to the specified file path
        /// </summary>
        /// <exception cref="HttpRequestException">Thrown when access to the download url has errored out </exception>
        /// <exception cref="ArgumentException">Thrown when the download url does not correctly reference a downloadable file </exception>
        /// <exception cref="DirectoryNotFound">Thrown when unable to manage the deletion/creaction of download directory </exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when unable to access the download directory location </exception>
        public static async Task Get(string url, string filePath)
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
