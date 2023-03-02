using CommunityToolkit.Mvvm.Input;
using NetCivitaiModelManager.Services;
using System.Windows.Input;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class MainVM : BaseVM
    {
        public FileDownoloadService FileDownoloadService { get; private set; }
        public HashService HashService { get; private set; }
        public ModelLoadService ModelLoadService { get; private set; }

        private OpenWindowService _openWindowService;
        public MainVM(FileDownoloadService fileDownoloadService, HashService hashService, ModelLoadService modelLoad, OpenWindowService openWindowService) 
        {
            FileDownoloadService = fileDownoloadService;
            HashService = hashService;
            ModelLoadService = modelLoad;
            _openWindowService = openWindowService;
        }

        [RelayCommand]
        public void OpenConfigWindow()
        {
            _openWindowService.OpenConfigWindow();
        }
    }
}
