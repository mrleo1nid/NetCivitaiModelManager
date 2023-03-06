using Aura.UI.Data;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetCivitaiModelManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
      
        public MainWindowViewModel() 
        {
         
        }

        public string Greeting => "Welcome to Avalonia!";
    }
}