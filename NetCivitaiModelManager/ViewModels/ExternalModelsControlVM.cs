using CivitaiApi.CivitaiDataContracts;
using CivitaiApi.CivitaiRequestParams;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NetCivitaiModelManager.Extensions;
using System.Diagnostics;
using NetCivitaiModelManager.Views;
using System.Threading;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class ExternalModelsControlVM : BaseVM
    {
        private readonly CivitaiService _service;
        private readonly OpenWindowService _openWindowService;
        private readonly FileDownoloadService _fileDownoloadService;
        [ObservableProperty]
        private List<Model> allModels = new List<Model>();
        [ObservableProperty]
        private int page;
        [ObservableProperty]
        private int maxPages;
        [ObservableProperty]
        private string? selectedPeriod;
        [ObservableProperty]
        private string? selectedSort;
        [ObservableProperty]
        private Model? selectedModel;
        [ObservableProperty]
        private ModelVersion? selectedVersion;
        [ObservableProperty]
        private File? selectedFile;
        private List<TypeToSelect> currentfilter = new List<TypeToSelect>();
        public ExternalModelsControlVM(CivitaiService service, OpenWindowService openWindowService, FileDownoloadService fileDownoloadService)
        {
            _service = service;
            Page = 1;
            MaxPages = 999;
            SelectedPeriod = Periods.FirstOrDefault();
            SelectedSort = Sort.FirstOrDefault();
            _openWindowService = openWindowService;
            _fileDownoloadService = fileDownoloadService;
            VisibleTagSelect = true;
            Tags = new List<string>() {"All"};
            SelectedTag = Tags.FirstOrDefault();
            Task.Factory.StartNew(async () => {
                await Task.Delay(1000);
                var tags = await _service.GetTagsAsync(new BaseRequestParams() { Limit = 19});
                Tags.AddRange(tags.Items.Select(x=>x.Name).ToList());
                await LoadModels();
            });
        }

        [RelayCommand]
        public void DownoloadModel(ModelVersion version)
        {
            if (SelectedModel != null)
            {
                if(version!=null)
                {
                    SelectedVersion = version;
                    if(SelectedVersion.Files.Count==1)
                    {
                        SelectedFile = SelectedVersion.Files.First();
                        LoadFile();
                    }
                    else if(SelectedVersion.Files.Count > 1)
                    {
                        _openWindowService.OpenSelectFileWindow(this);
                    }
                }
            }
        }
        [RelayCommand]
        public void LoadFile()
        {
            if (SelectedFile != null)
            {
                var modeltypeenum = selectedModel.Type.ToEnum<TypesEnum>();
                var folder = modeltypeenum.GetFolderByType(ConfigService.Config.WebUiFolderPath);
                var fullpath = System.IO.Path.Combine(folder, SelectedFile.Name);
                var shortname = System.IO.Path.GetFileNameWithoutExtension(fullpath);
                var imageurl = SelectedModel.DisplayImage;
                var imagepath = System.IO.Path.Combine(folder, shortname) + ".preview.png";
                _fileDownoloadService.AddAndStart(SelectedFile.DownloadUrl, fullpath, DownoloadType.Model,
                                       () => {
                                           _fileDownoloadService.AddAndStart(imageurl, imagepath, DownoloadType.Image);
                                       });
                SelectedModel = null; 
                SelectedVersion = null;
                _openWindowService.CloseWindow(_openWindowService.SelectFileWindow);
            }
            else
            {
                MessageBox.Show("Файл не выбран");
            }
        }
        [RelayCommand]
        public void CloseSelectFileWindow()
        {
            _openWindowService.CloseWindow(_openWindowService.SelectFileWindow);
        }

        [RelayCommand]
        private async Task OpenBrowser()
        {
            if (SelectedModel != null)
            {
                 string url = $"{ConfigService.Config.CivitaiBaseUrl}models/{SelectedModel.Id}";
                 Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
        }
       
        [RelayCommand]
        private async Task LoadModels()
        {
            var responce = await _service.GetModelsAsync(new GetModelsParams()
            {
                Page = Page,
                Limit = 32,
                Types = currentfilter.Select(x => x.Name).ToList(),
                Period = SelectedPeriod.Replace(" ", "") == "AllTime" ? null : SelectedPeriod.Replace(" ", ""),
                Sort = SelectedSort == "Highest Rated" ? null : SelectedSort,
                Tag = SelectedTag == "All" ? null : SelectedTag,
                Query = string.IsNullOrEmpty(SearchString) ? null : SearchString
            }) ;
            AllModels = responce?.Items;
            MaxPages = responce?.Metadata.TotalPages ?? 999;
        }
        [RelayCommand]
        private async Task NextPage()
        {
            if(Page < MaxPages)
            {
                Page++;
                await LoadModels();
            }
        }
        [RelayCommand]
        private async Task PrevPage()
        {
            if (Page >= 2)
            {
                Page--;
                await LoadModels();
            }
        }
        public void TagSelectionChanged(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(LoadModels);
        }
        public void FilterSelectionChanged(object sender, RoutedEventArgs e)
        {
            var elem = sender as System.Windows.Controls.ListBox;
            if (elem != null)
            {
                if (elem.SelectedItems.Count == 0)
                    currentfilter = new List<TypeToSelect>();
                else
                {
                    var lsit = new List<TypeToSelect>();
                    foreach (var item in elem.SelectedItems)
                    {
                        var it = item as TypeToSelect;
                        if (it != null)
                            lsit.Add(it);
                    }
                    currentfilter = lsit;
                }
                Task.Factory.StartNew(LoadModels);
            }
        }
        public void SelectionChanged(object sender, RoutedEventArgs e)
        {
           Task.Factory.StartNew(LoadModels);
        }
        [RelayCommand]
        private async Task SearchClick()
        {
            Task.Factory.StartNew(LoadModels);
        }
    }
}
