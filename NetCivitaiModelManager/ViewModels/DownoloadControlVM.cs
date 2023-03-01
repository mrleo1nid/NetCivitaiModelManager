using CommunityToolkit.Mvvm.ComponentModel;
using NetCivitaiModelManager.Extensions;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class DownoloadControlVM : BaseVM
    {
        [ObservableProperty]
        private List<DownoloadTask> filteredDownoload;
        [ObservableProperty]
        private string selectedtype;
        [ObservableProperty]
        private string selectedstate;

        public FileDownoloadService _fileDownoloadService { get; set; }
        public DownoloadControlVM(FileDownoloadService fileDownoloadService)
        {
            _fileDownoloadService = fileDownoloadService;
            Task.Factory.StartNew(LoadFromCash);
            FilteredDownoload = new List<DownoloadTask>();
            _fileDownoloadService.Downoloads.CollectionChanged += Downoloads_CollectionChanged;
            _fileDownoloadService.AddAndStart("https://imagecache.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/2d37f24e-f9fd-4900-29b7-e9d9548ce100/width=450", "protogenX34Photorealism_1.safetensors", Models.DownoloadType.Custom);
        }

        private void Downoloads_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs? e)
        {
            FilteredDownoload = FilterCollection(_fileDownoloadService.Downoloads);
        }

        public async void LoadFromCash()
        {
            await _fileDownoloadService.LoadDownoloadsFromCash();
        }
        private List<DownoloadTask> FilterCollection(ObservableCollection<DownoloadTask> tasks)
        {
            List<DownoloadTask> res = tasks.ToList();
            if(!string.IsNullOrEmpty(selectedtype) && selectedtype!=BaseSelect)
            {
                res = res.Where(x => x.Type == selectedtype.ToEnum<DownoloadType>()).ToList();
            }
            if (!string.IsNullOrEmpty(selectedstate) && selectedstate != BaseSelect)
            {
                res = res.Where(x => x.State == selectedstate.ToEnum<DownoloadStates>()).ToList();
            }
            return res;
        }
        public void SelectionChanged(object sender, RoutedEventArgs e)
        {
            FilteredDownoload = FilterCollection(_fileDownoloadService.Downoloads);
        }
    }
}
