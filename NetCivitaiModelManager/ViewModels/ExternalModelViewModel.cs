using CivitaiApiWrapper.DataContracts.Requsts;
using CivitaiApiWrapper.Services;
using DynamicData.Binding;
using ReactiveUI;
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
        private readonly PoliCivitaiService _poliCivitaiService;

        private readonly ReadOnlyObservableCollection<ModelCardViewModel> _models;
        public ReadOnlyObservableCollection<ModelCardViewModel> SearchResults => _models;
        public SearchFiltersViewModel SearchFiltersViewModel { get; set; }
        public PageSelectViewModel PageSelectViewModel { get; set; }
        public ExternalModelViewModel(PoliCivitaiService poliCivitaiService) 
        {
            PageSelectViewModel = new PageSelectViewModel();
            SearchFiltersViewModel = new SearchFiltersViewModel(true);
            _poliCivitaiService = poliCivitaiService;
            this.WhenAnyValue(vm => vm.SearchFiltersViewModel.SelectedSort, vm => vm.SearchFiltersViewModel.SelectedPeriod, vm => vm.SearchFiltersViewModel.SearchTerm)
                .Throttle(TimeSpan.FromSeconds(3)).Subscribe(async x => await Refresh());
            this.SearchFiltersViewModel.SelectedTypes
                .ToObservableChangeSet()
                .Subscribe(async x => await Refresh());

        }

        private async Task Refresh()
        {

        }
        private ModelsRequstParameters CreateRequest()
        {
            var request = new ModelsRequstParameters() 
            {

            };
            return request;
        }

    }
}
