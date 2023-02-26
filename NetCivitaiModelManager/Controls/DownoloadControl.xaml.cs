using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.ViewModels;
using System.Windows.Controls;

namespace NetCivitaiModelManager.Controls
{
    /// <summary>
    /// Логика взаимодействия для DownoloadControl.xaml
    /// </summary>
    public partial class DownoloadControl : UserControl
    {
        public DownoloadControl()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<DownoloadControlVM>();
        }
    }
}
