using CivitaiApi.CivitaiRequestParams;
using CivitaiApi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetCivitaiModelManager.Extensions;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using Refit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class LocalModelsControlVM : BaseVM
    {
        private CivitaiService _service;
        private LocalModelsService _localModelsService;
        private FileDownoloadService _filedownoloadService;
        public  HashService HashService { get; }
        public ModelLoadService ModelLoadService { get; }
        [ObservableProperty]
        private List<LocalModel> alllocalModels = new List<LocalModel>();
        [ObservableProperty]
        private List<LocalModel> filteredModels = new List<LocalModel>();
        [ObservableProperty]
        private LocalModel? selectedModel;
        [ObservableProperty]
        private string? cashСompute;
        [ObservableProperty]
        private string? modelFound;
        private List<TypesEnum> currentfilter = new List<TypesEnum>();
        public LocalModelsControlVM(CivitaiService civitaiService, LocalModelsService localModelsService,
            HashService hashservvice, ModelLoadService modelLoadService, FileDownoloadService filedownoloadService)
        {
            _service = civitaiService;
            _localModelsService = localModelsService;
            HashService = hashservvice;
            ModelLoadService = modelLoadService;
            _filedownoloadService = filedownoloadService;
            CashСompute = BaseSelectEnum.All.GetEnumDescription();
            ModelFound = BaseSelectEnum.All.GetEnumDescription();
            HashService.NotifyHashComplete += HashService_NotifyHashComplete;
            Task.Factory.StartNew(LoadModels);
        }

        private void HashService_NotifyHashComplete(LocalFile file)
        {
            var models = AlllocalModels.Where(x => x.LocalFile == file);
            if(models.Any())
            {
                foreach(var model in models)
                {
                    ModelLoadService.AddToQuque(model);
                }
                ModelLoadService.Start();
            }
        }

        [RelayCommand]
        private async Task SearchClick()
        {
            RefreshList();
        }
        [RelayCommand]
        private async Task UpdateImage()
        {
           if(SelectedModel != null)
            {
                var urlandpath = SelectedModel.CreateImagePath();
                if(!string.IsNullOrEmpty(urlandpath.Error)) 
                {
                    MessageBox.Show(urlandpath.Error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    _filedownoloadService.AddAndStart(urlandpath.Url, urlandpath.Path, DownoloadType.Image, 
                        () => {
                            SelectedModel.DisplayImage = urlandpath.Path;
                        });
                }
            }
        }
        [RelayCommand]
        private async Task OpenFolder()
        {
            if (SelectedModel != null)
            {
                if (!File.Exists(SelectedModel.LocalFile.FullName))
                {
                    return;
                }
                string argument = "/select, \"" + SelectedModel.LocalFile.FullName + "\"";

                Process.Start("explorer.exe", argument);
            }
        }
        [RelayCommand]
        private async Task OpenBrowser()
        {
            if (SelectedModel != null)
            {
                if(SelectedModel.ExternalModel!=null)
                {
                    string url = $"{ConfigService.Config.CivitaiBaseUrl}models/{SelectedModel.ExternalModel.ModelId}";
                    Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
                } 
            }
        }
        [RelayCommand]
        private async Task LoadModels()
        {
            AlllocalModels = new List<LocalModel>();
            FilteredModels = new List<LocalModel>();
            AlllocalModels = await _localModelsService.GetLocalModelsAsync();
            RefreshList();
            CalculateHash();
        }
        private void CalculateHash()
        {
            foreach (var model in AlllocalModels)
            {
                HashService.AddToQuque(model.LocalFile);
            }
            HashService.Start();
        }
        private void RefreshList()
        {
            if(!currentfilter.Any())
                FilteredModels = AlllocalModels;
            else
            {
                FilteredModels = AlllocalModels.Where(x => currentfilter.Contains(x.Type)).ToList();
            }
            if(!string.IsNullOrEmpty(SearchString))
            {
                var res = new List<LocalModel>();
                foreach(var model in FilteredModels)
                {
                    if(model.LocalFile != null && model.ExternalModel!=null)
                    {
                        if(model.LocalFile.Name.Contains(SearchString) || model.ExternalModel.Name.Contains(SearchString))
                            res.Add(model);
                    }
                    else if(model.LocalFile != null)
                    {
                        if (model.LocalFile.Name.Contains(SearchString))
                            res.Add(model);
                    }
                }
                FilteredModels = res;
            }
            if(CashСompute.ToEnum<BaseSelectEnum>()!=BaseSelectEnum.All)
            {
                bool chek = false;
                if(CashСompute.ToEnum<BaseSelectEnum>() == BaseSelectEnum.Yes) chek = true;
                if (CashСompute.ToEnum<BaseSelectEnum>() == BaseSelectEnum.No) chek = false;
                FilteredModels = FilteredModels.Where(x=>x.LocalFile.HashRedy==chek).ToList();
            }
            if (ModelFound.ToEnum<BaseSelectEnum>() != BaseSelectEnum.All)
            {
                bool chek = false;
                if (ModelFound.ToEnum<BaseSelectEnum>() == BaseSelectEnum.Yes) chek = true;
                if (ModelFound.ToEnum<BaseSelectEnum>() == BaseSelectEnum.No) chek = false;
                FilteredModels = FilteredModels.Where(x => x.ModelFound== chek).ToList();
            }
        }
        public void FilterSelectionChanged(object sender, RoutedEventArgs e)
        {
            var elem = sender as ListBox;
            if(elem != null)
            {
                if (elem.SelectedItems.Count == 0)
                    currentfilter = new List<TypesEnum>();
                else
                {
                    var lsit = new List<TypesEnum>();
                    foreach (var item in elem.SelectedItems)
                    {
                        var it = item as TypeToSelect;
                        if (it != null)
                            lsit.Add(it.Type);
                    }
                    currentfilter = lsit;
                }
                RefreshList();
            }
            
        }
        public void SelectionChanged(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }
    }
}
