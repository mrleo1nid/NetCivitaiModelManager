using CivitaiApiWrapper.DataContracts.Requsts;
using CivitaiApiWrapper.Services;
using DynamicData.Binding;
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
        [Reactive]  public ObservableCollection<ModelCardViewModel> SearchResults { get; set; }
        public SearchFiltersViewModel SearchFiltersViewModel { get; set; }
        public PageSelectViewModel PageSelectViewModel { get; set; }
        public ExternalModelViewModel(ExternalModelsService externalModelsService) 
        {
            SearchResults = new ObservableCollection<ModelCardViewModel>();
            PageSelectViewModel = new PageSelectViewModel();
            SearchFiltersViewModel = new SearchFiltersViewModel(true);
            _externalModelsService = externalModelsService;

            this.WhenAnyValue(vm => vm.PageSelectViewModel.CurrentPage)
                .Throttle(TimeSpan.FromMilliseconds(250)).Subscribe(async x => await Refresh());
            this.WhenAnyValue(vm => vm.SearchFiltersViewModel.SelectedSort, vm => vm.SearchFiltersViewModel.SelectedPeriod, vm => vm.SearchFiltersViewModel.SearchTerm)
                .Throttle(TimeSpan.FromSeconds(1)).Subscribe(async x => { PageSelectViewModel.ResetPage(); await Refresh(); });
            this.SearchFiltersViewModel.SelectedTypes
                .ToObservableChangeSet()
                .Subscribe(async x => {
                    PageSelectViewModel.ResetPage(); await Refresh();
                });
        }

        private async Task Refresh()
        {
           
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
