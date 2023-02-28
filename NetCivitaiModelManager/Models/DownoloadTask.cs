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
        [ObservableProperty]
        private int number;
        private HttpClient _httpClient;
        public string Url { get; private set; }
        public string FilePath { get; private set; }

        [ObservableProperty]
        private float downoloadProgress;
        [ObservableProperty]
        private DownoloadStates downoloadState;
        [ObservableProperty]
        private string name;
        public bool IsStarted { get; set; }
        
        private CancellationToken cancellationToken;
        public DownoloadTask(string url, string path, int number)
        {
            Url = url;
            FilePath = path;
            _httpClient = new HttpClient() { BaseAddress = new Uri(url) };
            cancellationToken = new CancellationToken();
            DownoloadState = DownoloadStates.Created;
            IsStarted = false;
            Number = number;
            Name = Path.GetFileName(FilePath);
        }
        public async Task StartAsync()
        {
            if(!IsStarted)
            {
                IsStarted = true;
                DownoloadState = DownoloadStates.Downoloading;
                IProgress<float> progress = new Progress<float>(e => DownoloadProgress = e);
                // Create a file stream to store the downloaded data.
                // This really can be any type of writeable stream.
                using (var file = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // Use the custom extension method below to download the data.
                    // The passed progress-instance will receive the download status updates.
                    await _httpClient.DownloadAsync(Url, file, progress, cancellationToken);
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
