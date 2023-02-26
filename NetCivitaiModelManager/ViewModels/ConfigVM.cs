using CommunityToolkit.Mvvm.ComponentModel;
using NetCivitaiModelManager.Services;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class ConfigVM : BaseVM
    {
        [ObservableProperty]
        private string? baseUrl;

        [ObservableProperty]
        private string? folderPath;
        public ConfigVM() 
        {
           LoadConfig();
        }
        public void SaveConfig()
        {
            ConfigService.Config.CivitaiBaseUrl = BaseUrl;
            ConfigService.Config.WebUiFolderPath = FolderPath;
            ConfigService.SaveConfig();
        }
        public void DefaultConfig()
        {
            BaseUrl = "";
            FolderPath = "";
        }
        private void LoadConfig()
        {
            BaseUrl = ConfigService.Config.CivitaiBaseUrl;
            FolderPath = ConfigService.Config.WebUiFolderPath;
        }
    }
}
