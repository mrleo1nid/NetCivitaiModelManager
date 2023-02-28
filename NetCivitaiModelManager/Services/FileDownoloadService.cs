using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetCivitaiModelManager.Services
{
    public partial class FileDownoloadService : ObservableObject
    {
        private ILogger<FileDownoloadService> _logger;

        [ObservableProperty]
        private List<DownoloadTask> downoloads = new List<DownoloadTask>();
        public FileDownoloadService(ILogger<FileDownoloadService> logger)
        {
            _logger = logger;
        }
        public async Task AddAndStart(string url, string path)
        {
            if(Downoloads.Where(x => x.Url == url && x.FilePath == path).Any())
            {
                MessageBox.Show("Данная загрузка уже существует");
                return;
            }
            else
            {
                var task = new DownoloadTask(url, path, Downoloads.Count+1);
                Downoloads.Add(task);
                Task.Factory.StartNew(task.StartAsync);
            }
        }
    }
}
