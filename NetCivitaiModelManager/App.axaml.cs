using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using NetCivitaiModelManager.ViewModels;
using NetCivitaiModelManager.Views;
using Splat;

namespace NetCivitaiModelManager
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Locator.Current.GetService<MainWindow>();
                desktop.MainWindow.DataContext = Locator.Current.GetService<MainWindowViewModel>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}