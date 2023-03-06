using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splat;
using NetCivitaiModelManager.ViewModels;

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
        }
    }
}
