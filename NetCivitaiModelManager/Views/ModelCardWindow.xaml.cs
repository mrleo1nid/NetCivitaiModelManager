using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Services;
using NetCivitaiModelManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NetCivitaiModelManager.Views
{
    /// <summary>
    /// Логика взаимодействия для ModelCardWindow.xaml
    /// </summary>
    public partial class ModelCardWindow : Window
    {
        public ModelCardWindow()
        {
            InitializeComponent();
            var openwind = Ioc.Default.GetRequiredService<OpenWindowService>();
            openwind.ModelCardWindow = this;
            DataContext = Ioc.Default.GetRequiredService<ModelCardVM>();
        }
    }
}
