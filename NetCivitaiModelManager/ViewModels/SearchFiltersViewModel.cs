using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using CivitaiApiWrapper.Enums;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetCivitaiModelManager.ViewModels
{
	public class SearchFiltersViewModel : ViewModelBase
	{
		[Reactive] public string SearchTerm { get; set; }
		[Reactive] public List<Types> TypesList { get; set; } 
        [Reactive] public ObservableCollection<Types> SelectedTypes { get; set; }
        public SearchFiltersViewModel()
		{
			TypesList = GetTypes().ToList();
            SearchTerm = string.Empty;
			SelectedTypes = GetTypes();
        }
		private ObservableCollection<Types> GetTypes()
		{
			return new ObservableCollection<Types>() {
			Types.Checkpoint,
            Types.TextualInversion,
            Types.AestheticGradient,
			Types.Hypernetwork,
			Types.LORA,
			Types.Controlnet,
			Types.Poses};
        }
    }
}