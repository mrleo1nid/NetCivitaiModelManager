using CommunityToolkit.Mvvm.ComponentModel;
using Downloader;
using NetCivitaiModelManager.Controls.Downoload;
using NetCivitaiModelManager.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private DateTime created;
        [ObservableProperty]
        private DownoloadStates state;
        [ObservableProperty]
        private double downoloadProgress;

        private string _url;
        private string _filePath;
        public DownloadService DownloadService { get; private set; }
        public DownoloadTask(string url, string path, int number, DownloadService service)
        {
            _url = url;
            _filePath = path;
            Number = number;
            Name = Path.GetFileName(path);
            Created = DateTime.Now;
            State = DownoloadStates.Created;
            DownloadService = service;
            DownoloadProgress = 0;
        }
        
        public async Task<DownoloadTask> Start()
        {
            await DownloadService.DownloadFileTaskAsync(_url, _filePath).ConfigureAwait(false);
            return this;
        }
        public void Cancel()
        {
            DownloadService.CancelAsync();
            State = DownoloadStates.Cancel;
        }
        public void Pause()
        {
            DownloadService.Pause();
            State = DownoloadStates.Paused;
        }
        public void Resume()
        {
            DownloadService.Resume();
            State = DownoloadStates.Downoloading;
        }
        public string GetIdenty()
        { 
            return  $"({_filePath} : {_url})";
        }
        public bool Equal(string url, string path)
        {
            if(_url == url && _filePath == path) return true;
            return false;
        }
    }
}
