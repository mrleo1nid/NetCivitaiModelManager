using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NetCivitaiModelManager.ViewModels
{
	public class SearchFiltersViewModel : ViewModelBase
	{
		[Reactive] public string SearchTerm { get; set; }
        public SearchFiltersViewModel()
		{
			SearchTerm = string.Empty;
		}
        

    }
}