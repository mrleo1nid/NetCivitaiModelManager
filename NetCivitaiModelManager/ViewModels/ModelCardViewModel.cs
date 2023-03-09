using CivitaiApiWrapper.DataContracts;
using NetCivitaiModelManager.Models;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public class ModelCardViewModel : ViewModelBase
    {
       [Reactive] public LocalModel? LocalModel { get; set; }
       [Reactive] public Model? ExternalModel { get; set; }
        public string Name => LocalModel != null ? LocalModel.Name : ExternalModel?.Name;
        public string? ImagePath => LocalModel != null ? LocalModel.LocalImagePath : ExternalModel?.ModelVersions.FirstOrDefault().Images.FirstOrDefault().Url;
        public ModelCardViewModel(LocalModel localModel) { LocalModel = localModel; }
        public ModelCardViewModel(Model extmodel) { ExternalModel = extmodel; }
    } 
}
