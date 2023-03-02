using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Services;
using NetCivitaiModelManager.ViewModels;
using NetCivitaiModelManager.Views;
using System.Windows;

namespace NetCivitaiModelManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var openwind = Ioc.Default.GetRequiredService<OpenWindowService>();
            openwind.MainWindow = this;
            DataContext = Ioc.Default.GetRequiredService<MainVM>();
        }
    }
}
