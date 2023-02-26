using CivitaiApi.CivitaiRequestParams;
using CivitaiApi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using NetCivitaiModelManager.Models;
using NetCivitaiModelManager.Services;
using Refit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetCivitaiModelManager.ViewModels
{
    public partial class LocalModelsControlVM : BaseVM
    {
        private CivitaiService _service;
        private LocalModelsService _localModelsService;

        [ObservableProperty]
        private List<LocalModel> alllocalModels = new List<LocalModel>();
        [ObservableProperty]
        private List<LocalModel> filteredModels = new List<LocalModel>();
        [ObservableProperty]
        private List<TypeToSelect> types = new List<TypeToSelect>() { new TypeToSelect() { Name = "Checkpoint" } , new TypeToSelect() { Name = "TextualInversion" }};
        private ObservableCollection<TypeToSelect> selectedtypes = new ObservableCollection<TypeToSelect>();
      
        public LocalModelsControlVM(CivitaiService civitaiService, LocalModelsService localModelsService)
        {
            _service = civitaiService;
            _localModelsService = localModelsService;
            Task.Factory.StartNew(LoadModels);
        }
        private async Task LoadModels()
        {
            AlllocalModels = await _localModelsService.GetLocalModelsAsync();
            FilteredModels = AlllocalModels;
            await _localModelsService.CalculateHash(AlllocalModels);
        }
        public void RefreshList(ListBox modelListBox)
        {

        }
        
    }
}
