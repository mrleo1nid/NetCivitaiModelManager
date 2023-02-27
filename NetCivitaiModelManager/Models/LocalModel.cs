using CivitaiApi.CivitaiDataContracts;
using CivitaiApi.CivitaiRequestParams;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public class LocalModel
    {
        public string? DisplayName { get; set; }
        public string? DisplayImage { get; set; }
        public LocalFile? LocalFile { get; set; }
        public ModelVersion? ExternalModel { get; set; }
        public string? TypeDisplay { get { return ((TypesEnum)Type).AsString(EnumFormat.Description); } }
        public TypesEnum Type { get; set; }

    }
}
