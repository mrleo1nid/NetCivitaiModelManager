using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.ViewModels;
using System.Windows.Controls;

namespace NetCivitaiModelManager.Controls
{
    /// <summary>
    /// Логика взаимодействия для ExternalModelsControl.xaml
    /// </summary>
    public partial class ExternalModelsControl : UserControl
    {
        public ExternalModelsControl()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<ExternalModelsControlVM>();
        }
    }
}
