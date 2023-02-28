using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<long> GetContentSizeAsync(this HttpClient client, string url)
        {
            using (var request = new System.Net.Http.HttpRequestMessage(HttpMethod.Get, url))
            {
                // In order to keep the response as small as possible, set the requested byte range to [0,0] (i.e., only the first byte)
                request.Headers.Range = new RangeHeaderValue(from: 0, to: 0);

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();

                    if (response.StatusCode != HttpStatusCode.PartialContent)
                        throw new System.Net.WebException($"expected partial content response ({System.Net.HttpStatusCode.PartialContent}), instead received: {response.StatusCode}");

                    var contentRange = response.Content.Headers.GetValues(@"Content-Range").Single();
                    var lengthString = System.Text.RegularExpressions.Regex.Match(contentRange, @"(?<=^bytes\s[0-9]+\-[0-9]+/)[0-9]+$").Value;
                    return long.Parse(lengthString);
                }
            }
        }
        public static async Task DownloadAsync(this HttpClient client, string requestUri, Stream destination, IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            // Get the http headers first to examine the content length
            using (var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
            {
                var contentLength = response.Content.Headers.ContentLength;

                using (var download = await response.Content.ReadAsStreamAsync())
                {

                    // Ignore progress reporting when no progress reporter was 
                    // passed or when the content length is unknown
                    if (progress == null || !contentLength.HasValue)
                    {
                        await download.CopyToAsync(destination);
                        return;
                    }

                    // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
                    var relativeProgress = new Progress<long>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));
                    // Use extension method to report progress while downloading
                    await download.CopyToAsync(destination, 81920, relativeProgress, cancellationToken);
                    progress.Report(1);
                }
            }
        }
    }
}
