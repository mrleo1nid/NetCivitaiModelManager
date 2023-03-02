using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetCivitaiModelManager.Extensions;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using System;
using System.Collections.Generic;
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
        private string? selectedState;
        [ObservableProperty]
        private string? selectedDownoloadType;
        [ObservableProperty]
        private List<DownoloadTask>? selectedDownoloadTasks;
        public FileDownoloadService FileDownoloadService { get; set; }
        public DownoloadControlVM(FileDownoloadService fileDownoloadService)
        {
            FileDownoloadService = fileDownoloadService;
            FileDownoloadService.Downoloads.CollectionChanged += Downoloads_CollectionChanged;
            SelectedDownoloadType = BaseSelect;
            SelectedState = BaseSelect;
            Task.Factory.StartNew(LoadFromCash);
            //Test();
        }
        public void DownoloadsSelectionChanged(object sender, RoutedEventArgs e)
        {
            var elem = sender as System.Windows.Controls.ListView;
            if (elem != null)
            {
                if (elem.SelectedItems.Count == 0)
                    SelectedDownoloadTasks = new List<DownoloadTask>();
                else
                {
                    var lsit = new List<DownoloadTask>();
                    foreach (var item in elem.SelectedItems)
                    {
                        var it = item as DownoloadTask;
                        if (it != null)
                            lsit.Add(it);
                    }
                    SelectedDownoloadTasks = lsit;
                }
            }
        }
        [RelayCommand]
        public  void StartSelected()
        {
            foreach(var task in SelectedDownoloadTasks)
            {
                if (task.State == DownoloadStates.Paused)
                    task.Resume();
                else if(task.State == DownoloadStates.Stopped)
                    task.Start();
                else if (task.State == DownoloadStates.Created)
                    task.Start();
                else if (task.State == DownoloadStates.Error)
                    task.Start();
            }
        }
        [RelayCommand]
        public  void PauseSelected()
        {
            foreach (var task in SelectedDownoloadTasks)
            {
                if (task.State == DownoloadStates.Downoloading)
                    task.Pause();
            }
        }
        [RelayCommand]
        public  void StopSelected()
        {
            foreach (var task in SelectedDownoloadTasks)
            {
                if (task.State == DownoloadStates.Downoloading)
                    task.Cancel();
                if (task.State == DownoloadStates.Created)
                    task.Cancel();
                if (task.State == DownoloadStates.Paused)
                    task.Cancel();
                if (task.State == DownoloadStates.Error)
                    task.Cancel();
            }
        }
        [RelayCommand]
        public  void RemoveSelected()
        {
            if (!SelectedDownoloadTasks.Any()) return;
            bool remfile = false;
            var mesres= MessageBox.Show("Вы хотите так же удалить загруженные файлы?","Удаление",MessageBoxButton.YesNoCancel,MessageBoxImage.Question);
            if (mesres == MessageBoxResult.Yes) { remfile = true; }
            else if (mesres == MessageBoxResult.No) { remfile = false; }
            else return;
            foreach (var task in SelectedDownoloadTasks)
            {
                FileDownoloadService.DeleteTask(task, remfile);
            }
        }
        public async void LoadFromCash()
        {
            await FileDownoloadService.LoadDownoloadsFromCash();
        }
        private void Downoloads_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            FilterDownoloads();
        }
        public void SelectionChanged(object sender, RoutedEventArgs e)
        {
            FilterDownoloads();
        }
        private void FilterDownoloads()
        {
            FilteredDownoload = FileDownoloadService.Downoloads.ToList();
            if (!string.IsNullOrEmpty(SelectedDownoloadType) && SelectedDownoloadType != BaseSelect)
                FilteredDownoload = FilteredDownoload.Where(x => x.Type == SelectedDownoloadType.ToEnum<DownoloadType>()).ToList();
                    if (!string.IsNullOrEmpty(SelectedState) && SelectedState != BaseSelect)
                FilteredDownoload = FilteredDownoload.Where(x => x.State == SelectedState.ToEnum<DownoloadStates>()).ToList();
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
