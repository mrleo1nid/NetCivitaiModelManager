using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Services;


namespace NetCivitaiModelManager.ViewModels
{
    public partial class BaseVM : ObservableObject
    {
        public ConfigService ConfigService { get; private set; }
        public BaseVM()
        {
            ConfigService = Ioc.Default.GetRequiredService<ConfigService>();
        }

        
    }
}
