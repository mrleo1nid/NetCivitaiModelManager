using CivitaiApi.CivitaiDataContracts;
using CivitaiApi.CivitaiRequestParams;
using CommunityToolkit.Mvvm.ComponentModel;
using NetCivitaiModelManager.Extensions;


namespace NetCivitaiModelManager.Models
{
    public partial class LocalModel : ObservableObject
    {
        public string? DisplayName { get; set; }
        public string? DisplayImage { get; set; }
        public LocalFile? LocalFile { get; set; }
        public ModelVersion? ExternalModel { get; set; }
        public string? TypeDisplay { get { return Type.GetEnumDescription(); ; } }
        public TypesEnum Type { get; set; }

        [ObservableProperty]
        private bool modelFound;
    }
}
