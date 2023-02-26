using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.ViewModels;
using System.Windows;

namespace NetCivitaiModelManager.Views
{
    /// <summary>
    /// Логика взаимодействия для ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private ConfigVM vm;
        public ConfigWindow()
        {
            InitializeComponent();
            vm = Ioc.Default.GetRequiredService<ConfigVM>();
            DataContext =vm;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы уверены что хотите выйти?", "Внимание", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                var parent = this.Owner as Window;
                parent.Focus();
                Close();
            }
              
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            vm.SaveConfig();
            Close();
        }

        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            vm.DefaultConfig();
        }
    }
}
