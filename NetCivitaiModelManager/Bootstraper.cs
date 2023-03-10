using Avalonia;
using CivitaiApiWrapper.Services;
using FluentAvalonia.Styling;
using NetCivitaiModelManager.Extensions;
using NetCivitaiModelManager.Services;
using NetCivitaiModelManager.ViewModels;
using NetCivitaiModelManager.Views;
using NLog;
using ReactiveUI;
using Refit;
using Splat;
using Splat.NLog;

namespace NetCivitaiModelManager
{
    public static class Bootstraper
    {
        public static AppBuilder CreateIoc(this AppBuilder appBuilder)
        {
            Locator.CurrentMutable.UseNLogWithWrappingFullLogger();
            SplatRegistrations.SetupIOC();
            RegisterServices();
            RegisterVM();
            return appBuilder;
        }
        public static void RegisterServices()
        {
            SplatRegistrations.RegisterLazySingleton<ConfigService>();
            SplatRegistrations.RegisterLazySingleton<LocalModelsService>();
            SplatRegistrations.RegisterLazySingleton<ExternalModelsService>();
            SplatRegistrations.RegisterLazySingleton<PoliCivitaiService>();
        }
        public static void RegisterVM()
        {
            SplatRegistrations.RegisterLazySingleton<MainWindowViewModel>();
       
            SplatRegistrations.RegisterLazySingleton<DownoloadsViewModel>();
            SplatRegistrations.RegisterLazySingleton<LocalModelsViewModel>();
            SplatRegistrations.RegisterLazySingleton<ExternalModelViewModel>();
            SplatRegistrations.RegisterLazySingleton<SettingsViewModel>();
            SplatRegistrations.RegisterLazySingleton<MainWindow>();
        }
        public static AppBuilder SetupExceptionHandling(this AppBuilder appBuilder)
        {
            // Подключим наш Observer-обработчик исключений
            RxApp.DefaultExceptionHandler = new ApcExceptionHandler(Locator.Current.GetService<ILogManager>().GetLogger<ApcExceptionHandler>());
            return appBuilder;
        }
    }
}
