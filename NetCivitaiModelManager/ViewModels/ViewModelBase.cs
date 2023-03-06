using Avalonia;
using Avalonia.Styling;
using FluentAvalonia.Styling;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace NetCivitaiModelManager.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public ViewModelBase()
        {
            CurrentAppTheme = Application.Current.ActualThemeVariant;
            this.WhenAnyValue(vm => vm.CurrentAppTheme).Subscribe(t => UpdateTheme(t));

        }
        private void UpdateTheme(ThemeVariant variant)
        {
            if(Application.Current.ActualThemeVariant != variant)
            {
                Application.Current.RequestedThemeVariant = variant;
            }
        }
        public ThemeVariant[] AppThemes { get; } =
         new[] { ThemeVariant.Light, ThemeVariant.Dark/*, FluentAvaloniaTheme.HighContrastTheme*/ };

        [Reactive] public ThemeVariant CurrentAppTheme { get; set; }
    }
  
}