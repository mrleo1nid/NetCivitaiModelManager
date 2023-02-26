using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class MainVM : BaseVM
    {
        public MainVM() 
        {
            OpenConfigWindowCommand = new RelayCommand(OpenConfigWindow);
        }

        public ICommand OpenConfigWindowCommand { get; }
        private void OpenConfigWindow()
        {
           
        }
    }
}
