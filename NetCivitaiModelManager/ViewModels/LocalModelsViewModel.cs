using CivitaiApiWrapper.DataContracts;
using CivitaiApiWrapper.Enums;
using DynamicData;
using DynamicData.Binding;
using FluentAvalonia.Core;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public class LocalModelsViewModel : ViewModelBase
    {

        public SearchFiltersViewModel SearchFiltersViewModel { get; set; }
        private readonly ReadOnlyObservableCollection<ModelCardViewModel> _models;
        public ReadOnlyObservableCollection<ModelCardViewModel> SearchResults => _models;
        public LocalModelsViewModel(LocalModelsService localModelsService) 
        {
            SearchFiltersViewModel = new SearchFiltersViewModel(false);

            var filterPredicate = this.WhenAnyValue(x => x.SearchFiltersViewModel.SearchTerm)
                          .Throttle(TimeSpan.FromMilliseconds(250), RxApp.TaskpoolScheduler)
                          .DistinctUntilChanged()
                          .Select(TermFilter);

            var typePredicate = this.SearchFiltersViewModel.SelectedTypes
                .ToObservableChangeSet()
                .Select(TypesFilter);

            
            localModelsService.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Filter(filterPredicate).Filter(typePredicate)
            .Sort(SortExpressionComparer<LocalModel>.Descending(model => model.Name))
            .Transform(x => new ModelCardViewModel(x))
            .Bind(out _models)
            .DisposeMany()
            .Subscribe();
        }
        Func<LocalModel, bool> TermFilter(string text) => term =>
        {
            return string.IsNullOrEmpty(text) || term.Name.ToLower().Contains(text.ToLower());
        };
        Func<LocalModel, bool> TypesFilter(IChangeSet<Types> types) => term =>
        {
            var result = true;
            if (SearchFiltersViewModel.SelectedTypes == null || SearchFiltersViewModel.SelectedTypes.Count() == 0) result = true;
            result = SearchFiltersViewModel.SelectedTypes.Contains(term.Type);
            return result;
        };
       
    }
}
