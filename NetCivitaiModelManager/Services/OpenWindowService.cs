using NetCivitaiModelManager.ViewModels;
using NetCivitaiModelManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetCivitaiModelManager.Services
{
    public class OpenWindowService
    {
        public MainWindow? MainWindow { get; set; }
        public ConfigWindow? ConfigWindow { get; set; }
        public SelectFileWindow? SelectFileWindow { get; set; }

        public void OpenConfigWindow()
        {
            ConfigWindow configWindow = ConfigWindow??  new ConfigWindow();
            configWindow.Show();
        }
        public void OpenSelectFileWindow(ExternalModelsControlVM vm)
        {
            SelectFileWindow window = SelectFileWindow ?? new SelectFileWindow(vm);
            window.Show();
        }
        public void CloseWindow(Window window)
        {
           window?.Close();
        }
    }
}
