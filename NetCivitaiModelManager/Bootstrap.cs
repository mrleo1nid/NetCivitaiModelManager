using Autofac;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using CivitaiApiWrapper.Services;
using NetCivitaiModelManager.ViewModels;
using NetCivitaiModelManager.Views;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager
{
    public static class Bootstrapper
    {
        public static AppBuilder BuildIoC(this AppBuilder appBuilder)
        {
            RegisterServices();
            RegisterViews();
            return appBuilder;
        }

        private static void RegisterServices()
        {
            
        }

        private static void RegisterViews()
        {
           
        }
    }
}
