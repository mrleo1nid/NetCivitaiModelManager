using NetCivitaiModelManager.Models;
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
        public LocalModel LocalModel { get; set; }
        public string Name => LocalModel.Name;
        public string? ImagePath => LocalModel.LocalImagePath;
        public ModelCardViewModel(LocalModel localModel) { LocalModel = localModel; }

    } 
}
