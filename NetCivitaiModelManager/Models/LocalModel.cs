using CivitaiApi.CivitaiDataContracts;
using CivitaiApi.CivitaiRequestParams;
using CommunityToolkit.Mvvm.ComponentModel;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public partial class LocalModel : ObservableObject
    {
        public string? DisplayName { get; set; }
        public string? DisplayImage { get; set; }
        public LocalFile? LocalFile { get; set; }
        public ModelVersion? ExternalModel { get; set; }
        public string? TypeDisplay { get { return ((TypesEnum)Type).AsString(EnumFormat.Description); } }
        public TypesEnum Type { get; set; }

        [ObservableProperty]
        private bool modelFound;
    }
}
