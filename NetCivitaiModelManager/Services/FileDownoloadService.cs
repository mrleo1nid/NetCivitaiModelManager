using CommunityToolkit.Mvvm.ComponentModel;
using Downloader;
using Microsoft.Extensions.Logging;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetCivitaiModelManager.Services
{
    public partial class FileDownoloadService : ObservableObject
    {
        private readonly string _keytocash = "FileDownoloadService_LastSavedDownoloadTasks";
        private ILogger<FileDownoloadService> _logger;
        private ConfigService _configService;
        private BlobCasheService _blobcash;
        [ObservableProperty]
        private List<DownoloadTask> downoloads = new List<DownoloadTask>();

        [ObservableProperty]
        private int allDownoloadsCount = 0;
        [ObservableProperty]
        private double allProgress = 0;
        [ObservableProperty]
        private bool workedExist = false;
        public FileDownoloadService(ILogger<FileDownoloadService> logger, ConfigService configService, BlobCasheService blobCasheService)
        {
            _logger = logger;
            _configService = configService;
            _blobcash = blobCasheService;
        }

        public async Task AddAndStart(string url, string path, Action<DownoloadTask>? completeaction = null)
        {
            var number = Downoloads.Count+1;
            var service = CreateDownloadService(_configService.DownloadConfiguration);
            if(Downoloads.Where(x=>x.Equal(url, path)).Any()) { return; }
            else Downoloads.Add(new DownoloadTask(url, path, number, service,completeaction).Start());
        }
        public async Task<DownoloadTask?> Add(string url, string path, Action<DownoloadTask>? completeaction = null)
        {
            var number = Downoloads.Count + 1;
            var service = CreateDownloadService(_configService.DownloadConfiguration);
            if (Downoloads.Where(x => x.Equal(url, path)).Any()) { return null; }
            else
            {
                var task = new DownoloadTask(url, path, number, service, completeaction);
                Downoloads.Add(task);
                return task;
            }    
        }
        public async Task SaveDownoloadsToCash()
        {
            await _blobcash.InsertDownoloadTask(_keytocash, Downoloads);
        }
        public async Task LoadDownoloadsFromCash()
        {
            Downoloads = await _blobcash.GetDownoloadTask(_keytocash);
        }
        private DownloadService CreateDownloadService(DownloadConfiguration config)
        {
            var downloadService = new DownloadService(config);
            downloadService.DownloadStarted += DownloadService_DownloadStarted;
            downloadService.ChunkDownloadProgressChanged += DownloadService_ChunkDownloadProgressChanged;
            downloadService.DownloadProgressChanged += DownloadService_DownloadProgressChanged;
            downloadService.DownloadFileCompleted += DownloadService_DownloadFileCompleted;
            return downloadService;
        }
        private DownoloadTask GetTaskByService(DownloadService service)
        {
            return Downoloads.Where(x=>x.DownloadService == service).FirstOrDefault();
        }
        private void DownloadService_DownloadFileCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var downoloader = sender as DownloadService;

            var task = GetTaskByService(downoloader);
            var identifier = "indefined";
            if(task != null)
            {
                identifier = task.GetIdenty();
                task.Complete();
            }
            if (e.Cancelled)
            {
                _logger.LogDebug(identifier + "CANCELED");
            }
            else if (e.Error != null)
            {
                _logger.LogDebug(identifier + "ERROR :" + e.Error.Message);
            }
            else
            {
                _logger.LogDebug(identifier + "DONE :");
            }
            UpdateInfo();
        }

        private void DownloadService_DownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            var downoloader = sender as DownloadService;

            var task = GetTaskByService(downoloader);
            if (task != null)
            {
                task.UpdateProgress(e);
            }
            UpdateInfo();
        }

        private void DownloadService_ChunkDownloadProgressChanged(object? sender, DownloadProgressChangedEventArgs e)
        {
            var downoloader = sender as DownloadService;

            var task = GetTaskByService(downoloader);
            if (task != null)
            {

            }
        }

        private void DownloadService_DownloadStarted(object? sender, DownloadStartedEventArgs e)
        {
            var downoloader = sender as DownloadService;

            var task = GetTaskByService(downoloader);
            if (task != null)
            {
                task.State = DownoloadStates.Downoloading;
            }
            
        }
        private void UpdateInfo()
        {
            var curdownloads = downoloads.Where(x => x.State == DownoloadStates.Downoloading);
            double currprogress = 0;
            foreach (var downoload in curdownloads)
                currprogress += downoload.DownoloadProgress;
            AllDownoloadsCount = curdownloads.Count();
            AllProgress = currprogress / AllDownoloadsCount;
            if(AllDownoloadsCount > 0) WorkedExist =true; else WorkedExist = false;
        }
    }
}
