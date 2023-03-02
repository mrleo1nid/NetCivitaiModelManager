using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace NetCivitaiModelManager.Controls.ExternalModels
{
    /// <summary>
    /// Логика взаимодействия для ExternalModelsControl.xaml
    /// </summary>
    public partial class ExternalModelsControl : UserControl
    {
        public ExternalModelsControl()
        {
            InitializeComponent();
            var vm = Ioc.Default.GetRequiredService<ExternalModelsControlVM>();
            DataContext = vm;
        }
    }
}
