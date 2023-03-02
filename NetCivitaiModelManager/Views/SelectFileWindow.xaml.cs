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
    /// Логика взаимодействия для SelectFileWindow.xaml
    /// </summary>
    public partial class SelectFileWindow : Window
    {
        private OpenWindowService service;
        public SelectFileWindow(ExternalModelsControlVM vm)
        {
            InitializeComponent();
            DataContext = vm;
            service = Ioc.Default.GetRequiredService<OpenWindowService>();
            service.SelectFileWindow = this;
            this.Owner = service.MainWindow;
            this.Top = Owner.Top + Owner.Height / 4;
            this.Left = Owner.Left + Owner.Width / 4;
            this.Closing += SelectFileWindow_Closing;
        }

        private void SelectFileWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            service.SelectFileWindow = null;
            this.Owner.Focus();
        }  
    }
}
