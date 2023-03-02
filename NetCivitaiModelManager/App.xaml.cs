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
using CivitaiApi.Extensions;

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
            Application.Current.Exit += Current_Exit;
            // Register services
            if (!_initialized)
            {
                _initialized = true;
                var configsevice = new ConfigService("config.json");
                CreateLogger(configsevice);
                var refitSettings = new RefitSettings() { };
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    //Logging
                    .AddLogging(loggingBuilder =>
                     loggingBuilder.AddSerilog(Log.Logger, dispose: true))
                    //Services

                    .AddSingleton<ConfigService>(configsevice)
                    .AddSingleton<SQLiteEncryptedBlobCache>(CreateBlob(configsevice))
                    .AddSingleton(RestService.For<ICivitaiService>(CreateHttpClient(configsevice), refitSettings))
                    .AddSingleton<CivitaiService>()
                    .AddSingleton<LocalModelsService>()
                    .AddSingleton<HashService>()
                    .AddSingleton<BlobCasheService>()
                    .AddSingleton<ModelLoadService>()
                    .AddSingleton<FileDownoloadService>()
                    .AddSingleton<OpenWindowService>()
                    //ViewModels
                    .AddTransient<MainVM>()
                    .AddTransient<ConfigVM>()
                    .AddTransient<DownoloadControlVM>()
                    .AddTransient<ExternalModelsControlVM>()
                    .AddTransient<LocalModelsControlVM>()
                    .BuildServiceProvider());
            }
        }
        private SQLiteEncryptedBlobCache CreateBlob(ConfigService configsevice)
        {
            string path = string.Empty;
            if (Uri.IsWellFormedUriString(configsevice.Config.CashPath, UriKind.Absolute)) 
                path = configsevice.Config.CashPath;
            else 
                path = Path.Combine(Environment.CurrentDirectory, configsevice.Config.CashPath);
            if(!Directory.Exists(path)) Directory.CreateDirectory(path);
            return new SQLiteEncryptedBlobCache(Path.Combine(path, configsevice.Config.CashFileName));
        }
        private void CreateLogger(ConfigService configsevice)
        {
            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Is(configsevice.Config.LogLevel)
                  .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                  .WithDefaultDestructurers()
                  .WithDestructurers(new[] { new ApiExceptionDestructurer() }))
                  .WriteTo.File(new JsonFormatter(renderMessage: true), $"logs\\log-{DateTime.Now.ToString("dd-MM-yyyy")}.txt", configsevice.Config.LogLevel)
                  .CreateLogger();
        }
        private HttpClient CreateHttpClient(ConfigService configsevice)
        {
            return new HttpClient(new HttpLoggingHandler(Log.Logger))
            {
                BaseAddress = new Uri(configsevice.Config.CivitaiBaseUrl),
                Timeout = TimeSpan.FromSeconds(90)
            };
        }
        private async void Current_Exit(object sender, ExitEventArgs e)
        {
            var downoloadService = Ioc.Default.GetRequiredService<FileDownoloadService>();
            downoloadService.SaveDownoloadsToCash();
            Log.CloseAndFlush();
            var sQLiteEncryptedBlobCache = Ioc.Default.GetRequiredService<SQLiteEncryptedBlobCache>();
            await sQLiteEncryptedBlobCache.Flush();
            await sQLiteEncryptedBlobCache.Shutdown;
        }
    }
}
