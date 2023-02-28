using CivitaiApi.CivitaiRequestParams;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Extensions;
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
            ,new TypeToSelect(TypesEnum.Hypernetwork)
            ,new TypeToSelect(TypesEnum.Controlnet)
            ,new TypeToSelect(TypesEnum.Poses)};
        [ObservableProperty]
        private List<string> periods = new List<string>() {
             PeriodEnum.AllTime.GetEnumDescription()
            ,PeriodEnum.Year.GetEnumDescription()
            ,PeriodEnum.Month.GetEnumDescription()
            ,PeriodEnum.Week.GetEnumDescription()
            ,PeriodEnum.Day.GetEnumDescription()};
        [ObservableProperty]  
        private List<string> sort = new List<string>() {
             SortEnum.HighestRated.GetEnumDescription()
            ,SortEnum.Newest.GetEnumDescription()
            ,SortEnum.MostDownloaded.GetEnumDescription()
            ,SortEnum.MostLiked.GetEnumDescription()
            ,SortEnum.MostDiscussed.GetEnumDescription()};
        public BaseVM()
        {
            ConfigService = Ioc.Default.GetRequiredService<ConfigService>();
        }

        
    }
}
