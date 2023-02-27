using CivitaiApi.CivitaiRequestParams;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using System.Collections.Generic;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class BaseVM : ObservableObject
    {
        public ConfigService ConfigService { get; private set; }
        [ObservableProperty]
        private List<TypeToSelect> types = new List<TypeToSelect>() {
             new TypeToSelect(TypesEnum.Checkpoint)
            ,new TypeToSelect(TypesEnum.TextualInversion)
            ,new TypeToSelect(TypesEnum.LORA)
            ,new TypeToSelect(TypesEnum.AestheticGradient)
            ,new TypeToSelect(TypesEnum.Hypernetwork)};
        public BaseVM()
        {
            ConfigService = Ioc.Default.GetRequiredService<ConfigService>();
        }

        
    }
}
