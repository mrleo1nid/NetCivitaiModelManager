using CommunityToolkit.Mvvm.ComponentModel;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class DownoloadControlVM : BaseVM
    {
        [ObservableProperty]
        private List<DownoloadTask> filteredDownoload;
        public FileDownoloadService _fileDownoloadService { get; set; }
        public DownoloadControlVM(FileDownoloadService fileDownoloadService)
        {
            _fileDownoloadService = fileDownoloadService;
            Task.Factory.StartNew(LoadFromCash);
            FilteredDownoload = new List<DownoloadTask>();
            _fileDownoloadService.Downoloads.CollectionChanged += Downoloads_CollectionChanged;
            _fileDownoloadService.AddAndStart("https://imagecache.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/2d37f24e-f9fd-4900-29b7-e9d9548ce100/width=450", "protogenX34Photorealism_1.safetensors", Models.DownoloadType.Custom);
        }

        private void Downoloads_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FilteredDownoload = _fileDownoloadService.Downoloads.ToList();
        }

        public async void LoadFromCash()
        {
            await _fileDownoloadService.LoadDownoloadsFromCash();
        }
    }
}
