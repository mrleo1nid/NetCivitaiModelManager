using Avalonia;
using Avalonia.Styling;
using FluentAvalonia.Styling;
using NetCivitaiModelManager.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;

namespace NetCivitaiModelManager.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public ConfigService ConfigService { get; private set; }
        public ViewModelBase()
        {
            ConfigService = Locator.Current.GetService<ConfigService>();
            CurrentAppTheme = Application.Current.ActualThemeVariant; 
        }
       
        public ThemeVariant[] AppThemes { get; } =
         new[] { ThemeVariant.Light, ThemeVariant.Dark/*, FluentAvaloniaTheme.HighContrastTheme*/ };

        [Reactive] public ThemeVariant CurrentAppTheme { get; set; }
    }
  
}