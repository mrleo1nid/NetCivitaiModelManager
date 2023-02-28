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
using CivitaiApi.Services;
using NetCivitaiModelManager.Extensions;
using System.Net.Http;
using Akavache;
using Akavache.Sqlite3;
using System.Reactive.Linq;
using System.IO;
using System.Threading.Tasks;

namespace NetCivitaiModelManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private bool _initialized;
        private readonly ConfigService configsevice;
        private SQLiteEncryptedBlobCache sQLiteEncryptedBlobCache;
        public App()
        {
            InitializeComponent();
            Application.Current.Exit += Current_Exit;
            // Register services
            if (!_initialized)
            {
                _initialized = true;
                configsevice = new ConfigService("config.json");
                CreateBlob();
                CreateLogger();
                var httpClient = new HttpClient(new HttpLoggingHandler(Log.Logger)) { BaseAddress = new Uri(configsevice.Config.CivitaiBaseUrl), Timeout = TimeSpan.FromSeconds(90) };
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    //Logging
                    .AddLogging(loggingBuilder =>
                     loggingBuilder.AddSerilog(Log.Logger, dispose: true))
                    //Services

                    .AddSingleton<ConfigService>(configsevice)
                    .AddSingleton<SQLiteEncryptedBlobCache>(sQLiteEncryptedBlobCache)
                    .AddSingleton(RestService.For<ICivitaiService>(httpClient))
                    .AddSingleton<CivitaiService>()
                    .AddSingleton<LocalModelsService>()
                    .AddSingleton<HashService>()
                    .AddSingleton<BlobCasheService>()
                    .AddSingleton<ModelLoadService>()

                    //ViewModels
                    .AddTransient<MainVM>()
                    .AddTransient<ConfigVM>()
                    .AddTransient<DownoloadControlVM>()
                    .AddTransient<ExternalModelsControlVM>()
                    .AddTransient<LocalModelsControlVM>()
                    .BuildServiceProvider());
            }
        }
        private void CreateBlob()
        {
            string path = string.Empty;
            if (Uri.IsWellFormedUriString(configsevice.Config.CashPath, UriKind.Absolute)) 
                path = configsevice.Config.CashPath;
            else 
                path = Path.Combine(Environment.CurrentDirectory, configsevice.Config.CashPath);
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
            sQLiteEncryptedBlobCache = new SQLiteEncryptedBlobCache(Path.Combine(path, configsevice.Config.CashFileName));
        }
        private void CreateLogger()
        {
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Is(configsevice.Config.LogLevel)
                  .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                  .WithDefaultDestructurers()
                  .WithDestructurers(new[] { new ApiExceptionDestructurer() }))
                  .WriteTo.File(new JsonFormatter(renderMessage: true), $"logs\\log-{DateTime.Now.ToString("dd-MM-yyyy")}.txt", configsevice.Config.LogLevel)
                  .CreateLogger();
        }
        private async void Current_Exit(object sender, ExitEventArgs e)
        {
            Log.CloseAndFlush();
            await sQLiteEncryptedBlobCache.Flush();
            await sQLiteEncryptedBlobCache.Shutdown;
        }
    }
}
