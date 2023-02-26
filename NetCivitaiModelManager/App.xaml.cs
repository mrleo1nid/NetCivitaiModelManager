using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using NetCivitaiModelManager.Services;
using NetCivitaiModelManager.ViewModels;
using Serilog.Formatting.Json;
using Serilog;
using System.Windows;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.Refit.Destructurers;
using Refit;
using System;

namespace NetCivitaiModelManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _initialized;

        public App()
        {
            InitializeComponent();

            // Register services
            if (!_initialized)
            {
                _initialized = true;
                var configsevice = new ConfigService("config.json");
                Log.Logger = new LoggerConfiguration()
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                    .WithDefaultDestructurers()
                    .WithDestructurers(new[] { new ApiExceptionDestructurer() }))
                    .WriteTo.File(new JsonFormatter(renderMessage: true),$"logs\\log-{DateTime.Now.ToString("dd-MM-yyyy")}.txt")
                    .CreateLogger();
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    //Logging
                    .AddLogging(loggingBuilder =>
                     loggingBuilder.AddSerilog(dispose: true))
                    //Services
                    .AddSingleton<ConfigService>(configsevice)
                    .AddSingleton(RestService.For<ICivitaiService>(configsevice.Config.CivitaiBaseUrl))
                    //ViewModels
                    .AddTransient<MainVM>()
                    .AddTransient<ConfigVM>()
                    .AddTransient<DownoloadControlVM>()
                    .AddTransient<ExternalModelsControlVM>()
                    .AddTransient<LocalModelsControlVM>()
                    .BuildServiceProvider());
            }

            
        }
    }
}
