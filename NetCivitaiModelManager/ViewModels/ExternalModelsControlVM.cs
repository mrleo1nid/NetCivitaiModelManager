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

namespace NetCivitaiModelManager.ViewModels
{
    public partial class ExternalModelsControlVM : BaseVM
    {
        private readonly CivitaiService _service;
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

        private List<TypeToSelect> currentfilter = new List<TypeToSelect>();
        public ExternalModelsControlVM(CivitaiService service)
        {
            _service = service;
            Page = 1;
            MaxPages = 999;
            SelectedPeriod = Periods.FirstOrDefault();
            SelectedSort = Sort.FirstOrDefault();

            Task.Factory.StartNew(LoadModels);
        }

        [RelayCommand]
        private async Task LoadModels()
        {
            var responce = await _service.GetModelsAsync(new GetModelsParams() 
            {
                Page = Page, Limit = 24, 
                Types = currentfilter.GetRowToRequest(),
                Period = SelectedPeriod.Replace(" ", "") == "AllTime" ? null : SelectedPeriod.Replace(" ", ""),
                Sort = SelectedSort== "Highest Rated" ? null : SelectedSort
            });
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
    }
}
