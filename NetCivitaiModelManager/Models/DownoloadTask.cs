using CommunityToolkit.Mvvm.ComponentModel;
using Downloader;
using NetCivitaiModelManager.Controls.Downoload;
using NetCivitaiModelManager.Extensions;
using Newtonsoft.Json;
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
        public Guid Id { get; set; }
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
        [JsonIgnore]
        private string? speed;
        [ObservableProperty]
        [JsonIgnore]
        private string? bytesReceived;
        [ObservableProperty]
        [JsonIgnore]
        private string? totalBytesToReceive;
        [ObservableProperty]
        [JsonIgnore]
        private string? time;
        [ObservableProperty]
        private string url;
        [ObservableProperty]
        private string filePath;
        [ObservableProperty]
        private DownoloadType type;
        public DownloadService DownloadService { get;  set; }
        private Action? _completeAction;
        public bool StopByUser { get; private set; }
        public bool StopToRemove { get; set; }
        public DownoloadTask(string url, string path, int number, DownloadService service, DownoloadType type, Action? completeaction = null)
        {
            Id = Guid.NewGuid();
            _completeAction = completeaction;
            Url = url;
            FilePath = path;
            Number = number;
            Name = Path.GetFileName(path);
            Created = DateTime.Now;
            State = DownoloadStates.Created;
            DownloadService = service;
            this.Type = type;
            StopByUser = false;
            StopToRemove = false;
        }
        
        public DownoloadTask Start()
        {
            DownloadService.DownloadFileTaskAsync(Url, FilePath);
            StopByUser = false;
            return this;
        }
        public void StartFromPack(DownloadPackage pack)
        {
            DownloadService.DownloadFileTaskAsync(pack);
            StopByUser = false;
        }
        public void Cancel()
        {
            DownloadService.CancelAsync();
            State = DownoloadStates.Stopped;
            StopByUser = true;
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
            StopByUser = false;
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
            ClearFields();
            Time = "Готово";
            _completeAction?.Invoke();
        }
        public void ClearFields() 
        {
            if (state == DownoloadStates.Completed)
                Time = "Готово";
            else time = null;
            Speed = null;
            TotalBytesToReceive = null;
            BytesReceived = null;
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
