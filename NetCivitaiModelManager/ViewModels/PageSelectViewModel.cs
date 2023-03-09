using NetCivitaiModelManager.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public class PageSelectViewModel : ViewModelBase
    {
        [Reactive] public int CurrentPage { get; set; }
        [Reactive] public int TotalPages { get; set; }

        public IReactiveCommand NextPageCommand { get; }
        public IReactiveCommand PrevPageCommand { get; }
        public PageSelectViewModel() 
        {
            ResetPage();
            NextPageCommand = ReactiveCommand.Create( () => { ButClick(); if (CurrentPage < TotalPages) CurrentPage++; });
            PrevPageCommand = ReactiveCommand.Create( () => { ButClick(); if (CurrentPage >1) CurrentPage--; });
        }
        public void ButClick() 
        {
        }
        public void ResetPage()
        {
            CurrentPage = 1;
            TotalPages = 999;
        }
    }
}
