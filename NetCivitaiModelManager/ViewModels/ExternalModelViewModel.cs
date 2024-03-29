﻿using CivitaiApiWrapper.DataContracts;
using CivitaiApiWrapper.DataContracts.Requsts;
using CivitaiApiWrapper.Services;
using DynamicData;
using DynamicData.Binding;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
  
    public class ExternalModelViewModel : ViewModelBase
    {
        private readonly ExternalModelsService _externalModelsService;

        private readonly ReadOnlyObservableCollection<ModelCardViewModel> _models;
        public ReadOnlyObservableCollection<ModelCardViewModel> SearchResults => _models;
        [Reactive] public ModelCardViewModel SelectedModel { get;  set; }
        public SearchFiltersViewModel SearchFiltersViewModel { get; set; }
        public PageSelectViewModel PageSelectViewModel { get; set; }
        public int TotalPages => _externalModelsService.TotalPages;
        public int TotalItems => _externalModelsService.TotalItems;
        [Reactive] public bool InProgress { get; set; }
        [Reactive] public bool NotFound { get; set; }
        public ExternalModelViewModel(ExternalModelsService externalModelsService) 
        {
            PageSelectViewModel = new PageSelectViewModel();
            SearchFiltersViewModel = new SearchFiltersViewModel(true);
            _externalModelsService = externalModelsService;

            this.WhenAnyValue(vm => vm.PageSelectViewModel.CurrentPage)
                .Throttle(TimeSpan.FromMilliseconds(250)).DistinctUntilChanged().Subscribe(async x => await Refresh());
            this.WhenAnyValue(vm => vm.SearchFiltersViewModel.SelectedSort, vm => vm.SearchFiltersViewModel.SelectedPeriod, vm => vm.SearchFiltersViewModel.SearchTerm)
                .Throttle(TimeSpan.FromSeconds(1)).DistinctUntilChanged().Subscribe(async x => { PageSelectViewModel.ResetPage(); await Refresh(); });
            this.SearchFiltersViewModel.SelectedTypes
                .ToObservableChangeSet().DistinctUntilChanged()
                .Subscribe(async x => {
                    PageSelectViewModel.ResetPage(); await Refresh();
                });
            this.WhenAnyValue(vm => vm._externalModelsService.InProgress)
                .Subscribe(x => { InProgress = _externalModelsService.InProgress; if (InProgress == false && _models.Count == 0) NotFound = true; else NotFound = false; });
            _externalModelsService.Connect()
              .ObserveOn(RxApp.MainThreadScheduler)
              .Transform(x => new ModelCardViewModel(x))
              .Bind(out _models)
              .DisposeMany()
              .Subscribe();
        }

        private async Task Refresh()
        {
           await _externalModelsService.AddRequest(CreateRequest());
           PageSelectViewModel.TotalPages = _externalModelsService.TotalPages;
        }
        private ModelsRequstParameters CreateRequest()
        {
            var request = new ModelsRequstParameters()
            {
                Limit = ConfigService.Config.ExternalModelInPage,
                Sort = SearchFiltersViewModel.SelectedSort,
                Period = SearchFiltersViewModel.SelectedPeriod,
                Query = string.IsNullOrEmpty(SearchFiltersViewModel.SearchTerm) ? null : SearchFiltersViewModel.SearchTerm,
                Page = PageSelectViewModel.CurrentPage,
                Types = SearchFiltersViewModel.SelectedTypes.Count() == 0 ? null : SearchFiltersViewModel.SelectedTypes.ToList()
            };
            return request;
        }

    }
}
