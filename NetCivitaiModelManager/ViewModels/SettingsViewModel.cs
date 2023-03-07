using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using DynamicData.Binding;
using NetCivitaiModelManager.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
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
        public ThemeVariant[] AppThemes { get; } = new[] { ThemeVariant.Light, ThemeVariant.Dark};
        [Reactive] public ThemeVariant CurrentAppTheme { get; set; }
        [Reactive] public string WebUiFolderPath { get; set; }
        [Reactive] public string BaseUrl { get; set; }
        [Reactive] public string ApiKey { get; set; }
        [Reactive] public string CashPath { get; set; }
        [Reactive] public string CashFileName { get; set; }

        public IReactiveCommand OpenFolderWebUiPath { get; }
        public IReactiveCommand OpenCashFolderPath { get; }
        public SettingsViewModel() 
        {
            LoadFromConfig();
            OpenFolderWebUiPath = ReactiveCommand.Create(async () => { WebUiFolderPath = await OpenFolderDialog() ?? WebUiFolderPath; });
            OpenCashFolderPath = ReactiveCommand.Create(async () => { CashPath = await OpenFolderDialog() ?? CashPath; });
            this.WhenAnyValue(vm => vm.CurrentAppTheme).Subscribe(t => UpdateTheme(t));
            this.WhenAnyValue(
                vm => vm.CurrentAppTheme,
                vm => vm.WebUiFolderPath,
                vm => vm.BaseUrl,
                vm => vm.ApiKey,
                vm => vm.CashPath,
                vm => vm.CashFileName).Throttle(TimeSpan.FromSeconds(3)).Subscribe(_ => SaveToConfig());
        }
        private void UpdateTheme(ThemeVariant variant)
        {
            if (Application.Current.ActualThemeVariant != variant)
            {
                Application.Current.RequestedThemeVariant = variant;
            }
        }
        private void SaveToConfig()
        {
            ConfigService.Config.CurrentTheme = CurrentAppTheme.Key.ToString();
            ConfigService.Config.WebUiFolderPath = WebUiFolderPath;
            ConfigService.Config.CivitaiBaseUrl = BaseUrl;
            ConfigService.Config.ApiKey = ApiKey;
            ConfigService.Config.CashPath = CashPath;
            ConfigService.Config.CashFileName = CashFileName;
            ConfigService.SaveConfig();
        }
        private void LoadFromConfig()
        {
            CurrentAppTheme = Application.Current.ActualThemeVariant;
            WebUiFolderPath = ConfigService.Config.WebUiFolderPath;
            BaseUrl = ConfigService.Config.CivitaiBaseUrl;
            ApiKey = ConfigService.Config.ApiKey;
            CashPath = ConfigService.Config.CashPath;
            CashFileName = ConfigService.Config.CashFileName;
        }
       
        public async Task<string> OpenFolderDialog()
        {
            var dialog = new OpenFolderDialog();
            var result = await dialog.ShowAsync(Locator.Current.GetService<MainWindow>());
            return result;
        }
    }
}
