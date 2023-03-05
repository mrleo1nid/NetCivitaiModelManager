using Splat;
namespace NetCivitaiModelManager.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() { }
        [DependencyInjectionConstructor]
        public MainWindowViewModel(CustomVM customVM) 
        {

        }
        public string Greeting => "Welcome to Avalonia!";
    }
}