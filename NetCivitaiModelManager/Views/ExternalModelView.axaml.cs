using Avalonia.Controls;
using System.ComponentModel;
using System;
using ReactiveUI;
using System.Reflection;

namespace NetCivitaiModelManager.Views
{
    public partial class ExternalModelView : UserControl
    {
        public ExternalModelView()
        {
            InitializeComponent();
            var lb = this.FindControl<ListBox>("cards");
            lb.AutoScrollToSelectedItem = true;
        }
    }
}
