using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Services;
using NetCivitaiModelManager.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace NetCivitaiModelManager.Views
{
    /// <summary>
    /// Логика взаимодействия для ConfigWindow.xaml
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private ConfigVM vm;
        private OpenWindowService service;
        public ConfigWindow()
        {
            InitializeComponent();
            service = Ioc.Default.GetRequiredService<OpenWindowService>();
            service.ConfigWindow = this;
            this.Owner = service.MainWindow;
            this.Top = Owner.Top + Owner.Height / 4;
            this.Left = Owner.Left + Owner.Width / 4;
            vm = Ioc.Default.GetRequiredService<ConfigVM>();
            DataContext =vm;
            this.Closing += SelectFileWindow_Closing;
        }

        private void SelectFileWindow_Closing(object? sender, CancelEventArgs e)
        {
            service.ConfigWindow = null;
            this.Owner.Focus();
        }
    }
}
