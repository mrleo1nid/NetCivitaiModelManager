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
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public class LocalModelsViewModel : ViewModelBase
    {

        private readonly ReadOnlyObservableCollection<ModelCardViewModel> _models;
        public ReadOnlyObservableCollection<ModelCardViewModel> SearchResults => _models;
        public LocalModelsViewModel(LocalModelsService localModelsService) 
        {
            localModelsService.Connect()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Transform(x => new ModelCardViewModel(x))
            .Bind(out _models)
            .DisposeMany()
            .Subscribe();
        }

       
    }
}
