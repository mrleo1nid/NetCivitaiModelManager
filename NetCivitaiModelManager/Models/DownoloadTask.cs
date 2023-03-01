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
        [ObservableProperty]
        private string textProgress;
        [ObservableProperty]
        private string? speed;
        [ObservableProperty]
        private string? bytesReceived;
        [ObservableProperty]
        private string? totalBytesToReceive;
        [ObservableProperty]
        private string? time;
        [ObservableProperty]
        private string url;
        [ObservableProperty]
        private string filePath;
        public DownloadService DownloadService { get; private set; }
        private Action<DownoloadTask>? _completeAction;
        public DownoloadTask(string url, string path, int number, DownloadService service, Action<DownoloadTask>? completeaction = null)
        {
            _completeAction = completeaction;
            Url = url;
            FilePath = path;
            Number = number;
            Name = Path.GetFileName(path);
            Created = DateTime.Now;
            State = DownoloadStates.Created;
            DownloadService = service;
        }
        
        public DownoloadTask Start()
        {
            DownloadService.DownloadFileTaskAsync(Url, FilePath).ConfigureAwait(false);
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
            return  $"({FilePath} : {Url})";
        }
        public bool Equal(string url, string path)
        {
            if(Url == url && FilePath == path) return true;
            return false;
        }
        public void Complete()
        {
            State = DownoloadStates.Completed;
            Time = "Готово";
            Speed = null;
            TotalBytesToReceive = null;
            BytesReceived = null;
            _completeAction?.Invoke(this);
        }
        public void UpdateProgress(DownloadProgressChangedEventArgs e)
        {
            double nonZeroSpeed = e.BytesPerSecondSpeed + 0.0001;
            int estimateTime = (int)((e.TotalBytesToReceive - e.ReceivedBytesSize) / nonZeroSpeed);
            bool isMinutes = estimateTime >= 60;
            string timeLeftUnit = "секунд";

            if (isMinutes)
            {
                timeLeftUnit = "минут";
                estimateTime /= 60;
            }

            if (estimateTime < 0)
            {
                estimateTime = 0;
                timeLeftUnit = "неизвестно";
            }
            Time = $"{estimateTime} {timeLeftUnit} осталось";
            Speed = e.BytesPerSecondSpeed.CalcMemoryMensurableUnit();
            BytesReceived = e.ReceivedBytesSize.CalcMemoryMensurableUnit();
            TotalBytesToReceive = e.TotalBytesToReceive.CalcMemoryMensurableUnit();
            TextProgress = $"{e.ProgressPercentage:F2}".Replace("/", ".") + " %";
            DownoloadProgress = e.ProgressPercentage;
        }
    }
}
