﻿using CivitaiApi.CivitaiRequestParams;
using CivitaiApi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using Refit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        private HashService _hashService;

        [ObservableProperty]
        private List<LocalModel> alllocalModels = new List<LocalModel>();
        [ObservableProperty]
        private List<LocalModel> filteredModels = new List<LocalModel>();
       
        private List<TypesEnum> currentfilter = new List<TypesEnum>();
        public LocalModelsControlVM(CivitaiService civitaiService, LocalModelsService localModelsService, HashService hashservvice)
        {
            _service = civitaiService;
            _localModelsService = localModelsService;
            _hashService = hashservvice;
            Task.Factory.StartNew(LoadModels);
        }

        [RelayCommand]
        private async Task LoadModels()
        {
            AlllocalModels = new List<LocalModel>();
            FilteredModels = new List<LocalModel>();
            AlllocalModels = await _localModelsService.GetLocalModelsAsync();
            RefreshList();
            await Task.Factory.StartNew(CalculateHash);
        }
        private async Task CalculateHash()
        {
            foreach (var model in AlllocalModels)
                _hashService.AddToQuque(model);
            _hashService.Start();
        }
        private void RefreshList()
        {
            if(!currentfilter.Any())
                FilteredModels = AlllocalModels;
            else
            {
                FilteredModels = AlllocalModels.Where(x => currentfilter.Contains(x.Type)).ToList();
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
    }
}
