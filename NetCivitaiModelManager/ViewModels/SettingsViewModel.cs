using Avalonia;
using Avalonia.Styling;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel() 
        {
            this.WhenAnyValue(vm => vm.CurrentAppTheme).Subscribe(t => UpdateTheme(t));
            this.WhenAnyValue(vm => vm.CurrentAppTheme).Subscribe(_ => RefreshConfig());
        }
        private void UpdateTheme(ThemeVariant variant)
        {
            if (Application.Current.ActualThemeVariant != variant)
            {
                Application.Current.RequestedThemeVariant = variant;
            }
        }
        private void RefreshConfig()
        {
            ConfigService.Config.CurrentTheme = CurrentAppTheme.Key.ToString();
            ConfigService.SaveConfig();
        }
    }
}
