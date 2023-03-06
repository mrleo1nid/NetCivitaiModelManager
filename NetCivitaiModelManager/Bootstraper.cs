using Avalonia;
using FluentAvalonia.Styling;
using NetCivitaiModelManager.ViewModels;
using Splat;

namespace NetCivitaiModelManager
{
    public static class Bootstraper
    {
        public static AppBuilder CreateIoc(this AppBuilder appBuilder)
        {
            SplatRegistrations.SetupIOC();
            RegisterServices();
            RegisterVM();
            return appBuilder;
        }
        public static void RegisterServices()
        {
           
        }
        public static void RegisterVM()
        {
            SplatRegistrations.RegisterLazySingleton<MainWindowViewModel>();
            SplatRegistrations.RegisterLazySingleton<DownoloadsViewModel>();
            SplatRegistrations.RegisterLazySingleton<LocalModelsViewModel>();
            SplatRegistrations.RegisterLazySingleton<ExternalModelViewModel>();
            SplatRegistrations.RegisterLazySingleton<SettingsViewModel>();
        }
    }
}
