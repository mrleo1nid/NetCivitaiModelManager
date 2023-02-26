using CivitaiApi.CivitaiDataContracts;
using CivitaiApi.CivitaiRequestParams;
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
        public Model? ExternalModel { get; set; }
        
        public TypesEnum Type { get; set; }

    }
}
