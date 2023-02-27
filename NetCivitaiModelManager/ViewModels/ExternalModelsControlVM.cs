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
using System.Windows.Controls;
using System.Windows;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class ExternalModelsControlVM : BaseVM
    {
        private readonly CivitaiService _service;
        [ObservableProperty]
        private List<Model> allModels = new List<Model>();
        private List<TypeToSelect> currentfilter = new List<TypeToSelect>();
        [ObservableProperty]
        private int page;
        [ObservableProperty]
        private int maxPages;
        public ExternalModelsControlVM(CivitaiService service)
        {
            _service = service;
            page = 1;
            maxPages = 999;
            Task.Factory.StartNew(LoadModels);
        }

        [RelayCommand]
        private async Task LoadModels()
        {
            var types = currentfilter.Select(x => x.Name.Replace(" ", "")).ToList();
            if (!types.Any()) types = null;
            var responce = await _service.GetModelsAsync(new GetModelsParams() { Page = Page, Limit = 24, Types = types });
            AllModels = responce?.Items;
            MaxPages = responce?.Metadata.TotalPages ?? 999;
        }
        [RelayCommand]
        private async Task NextPage()
        {
            if(page < maxPages)
            {
                Page++;
                await Task.Factory.StartNew(LoadModels);
            }
        }
        [RelayCommand]
        private async Task PrevPage()
        {
            if (page >= 2)
            {
                Page--;
                await Task.Factory.StartNew(LoadModels);
            }
        }
        public void FilterSelectionChanged(object sender, RoutedEventArgs e)
        {
            var elem = sender as ListBox;
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
        
    }
}
