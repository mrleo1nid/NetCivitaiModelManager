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

        public FileDownoloadService FileDownoloadService { get; set; }
        public DownoloadControlVM(FileDownoloadService fileDownoloadService)
        {
            FileDownoloadService = fileDownoloadService;
            FileDownoloadService.Downoloads.CollectionChanged += Downoloads_CollectionChanged;
            Task.Factory.StartNew(Test);
        }

        private void Downoloads_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
           FilteredDownoload = FileDownoloadService.Downoloads.ToList();
        }

        public void Test()
        {
           FileDownoloadService
                .AddAndStart("https://civitai.com/api/download/models/4048?type=Pruned%20Model&format=SafeTensor", "D:\\backup\\protogenX34Photorealism_1.safetensors");
             FileDownoloadService
               .AddAndStart("https://civitai.com/api/download/models/4048?type=Pruned%20Model&format=SafeTensor", "D:\\backup\\protogenX34Photorealism_2.safetensors");
             FileDownoloadService
               .AddAndStart("https://civitai.com/api/download/models/4048?type=Pruned%20Model&format=SafeTensor", "D:\\backup\\protogenX34Photorealism_3.safetensors");
        }
    }
}
