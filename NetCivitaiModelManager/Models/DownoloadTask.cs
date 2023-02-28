using CommunityToolkit.Mvvm.ComponentModel;
using NetCivitaiModelManager.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public partial class DownoloadTask : ObservableObject
    {
        private HttpClient _httpClient;
        public string Url { get; private set; }
        public string Path { get; private set; }

        [ObservableProperty]
        private IProgress<float> downoloadProgress;
        [ObservableProperty]
        private DownoloadStates downoloadState;
        public bool IsStarted { get; set; }

        private CancellationToken cancellationToken;
        public DownoloadTask(string url, string path)
        {
            Url = url;
            Path = path;
            _httpClient = new HttpClient() { BaseAddress = new Uri(url) };
            cancellationToken = new CancellationToken();
            DownoloadState = DownoloadStates.Created;
            IsStarted = false;
        }
        public async Task StartAsync()
        {
            if(!IsStarted)
            {
                IsStarted = true;
                DownoloadState = DownoloadStates.Downoloading;
                // Create a file stream to store the downloaded data.
                // This really can be any type of writeable stream.
                using (var file = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Use the custom extension method below to download the data.
                    // The passed progress-instance will receive the download status updates.
                    await _httpClient.DownloadAsync(Url, file, DownoloadProgress, cancellationToken);
                }
                DownoloadComplete();
            }
           
        }
        private void DownoloadComplete()
        {
            DownoloadState = DownoloadStates.Completed;
            _httpClient.Dispose();
        }
        public void StopAsync()
        {
            DownoloadState = DownoloadStates.Stoped;
            cancellationToken.ThrowIfCancellationRequested();
            IsStarted = false;
        }
    }
}
