using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetCivitaiModelManager.Services;
using System.Windows;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class ConfigVM : BaseVM
    {
        [ObservableProperty]
        private string? baseUrl;

        [ObservableProperty]
        private string? folderPath;

        private OpenWindowService _openWindowService;
        public ConfigVM(OpenWindowService openWindowService) 
        {
           LoadConfig();
            _openWindowService = openWindowService;
        }

        [RelayCommand]
        public void Save()
        {
            ConfigService.Config.CivitaiBaseUrl = BaseUrl;
            ConfigService.Config.WebUiFolderPath = FolderPath;
            ConfigService.SaveConfig();
            _openWindowService.CloseWindow(_openWindowService.ConfigWindow);
        }
        [RelayCommand]
        public void ReturnToDefault()
        {
            BaseUrl = "";
            FolderPath = "";
        }
        [RelayCommand]
        public void CloseWindow()
        {
            var res = MessageBox.Show("Вы уверены что хотите выйти?", "Внимание", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                _openWindowService.CloseWindow(_openWindowService.ConfigWindow);
            }
        }
        private void LoadConfig()
        {
            BaseUrl = ConfigService.Config.CivitaiBaseUrl;
            FolderPath = ConfigService.Config.WebUiFolderPath;
        }
    }
}
