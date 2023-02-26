using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.ViewModels;
using System.Windows.Controls;

namespace NetCivitaiModelManager.Controls
{
    /// <summary>
    /// Логика взаимодействия для LocalModelsControl.xaml
    /// </summary>
    public partial class LocalModelsControl : UserControl
    {
        public LocalModelsControl()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetRequiredService<LocalModelsControlVM>();
        }
    }
}
