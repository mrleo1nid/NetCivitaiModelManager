using CommunityToolkit.Mvvm.DependencyInjection;
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
            DataContext = Ioc.Default.GetRequiredService<MainVM>();
        }

        private void OpenSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigWindow configWindow = new ConfigWindow();
            configWindow.Owner = this;
            configWindow.Top = this.Top + this.Height/4;
            configWindow.Left = this.Left + this.Width / 4;
            configWindow.Show();
        }
    }
}
