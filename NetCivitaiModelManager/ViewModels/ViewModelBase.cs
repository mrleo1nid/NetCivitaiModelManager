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
            if(ConfigService.Config.CurrentTheme == FluentAvaloniaTheme.LightModeString) { CurrentAppTheme = ThemeVariant.Light; }
            else if (ConfigService.Config.CurrentTheme == FluentAvaloniaTheme.DarkModeString) { CurrentAppTheme = ThemeVariant.Dark;}
            else { CurrentAppTheme = Application.Current.ActualThemeVariant; }
            
            this.WhenAnyValue(vm => vm.CurrentAppTheme).Subscribe(t => UpdateTheme(t));


        }
        private void UpdateTheme(ThemeVariant variant)
        {
            if(Application.Current.ActualThemeVariant != variant)
            {
                Application.Current.RequestedThemeVariant = variant;
                ConfigService.Config.CurrentTheme = variant.ToString();
                ConfigService.SaveConfig();
            }
        }
        public ThemeVariant[] AppThemes { get; } =
         new[] { ThemeVariant.Light, ThemeVariant.Dark/*, FluentAvaloniaTheme.HighContrastTheme*/ };

        [Reactive] public ThemeVariant CurrentAppTheme { get; set; }
    }
  
}