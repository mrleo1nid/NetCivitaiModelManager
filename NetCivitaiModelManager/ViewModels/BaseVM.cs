using CivitaiApi.CivitaiRequestParams;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Extensions;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using System.Collections.Generic;
using System.Windows;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class BaseVM : ObservableObject
    {
        public const string BaseSelect = "Все";
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
        [ObservableProperty]
        private List<string> states = new List<string>() {
             BaseSelect
            ,DownoloadStates.Downoloading.GetEnumDescription()
            ,DownoloadStates.Created.GetEnumDescription()
            ,DownoloadStates.Stopped.GetEnumDescription()
            ,DownoloadStates.Completed.GetEnumDescription()
            ,DownoloadStates.Error.GetEnumDescription()};
        [ObservableProperty]
        private List<string> downoloadTypes = new List<string>() {
             BaseSelect
            ,DownoloadType.Image.GetEnumDescription()
            ,DownoloadType.Model.GetEnumDescription()
            ,DownoloadType.Custom.GetEnumDescription() };
        [ObservableProperty]
        private List<string> prioritiModelList = new List<string>() { " ", " ", " " };
        public BaseVM()
        {
            ConfigService = Ioc.Default.GetRequiredService<ConfigService>();
        }

        
    }
}
