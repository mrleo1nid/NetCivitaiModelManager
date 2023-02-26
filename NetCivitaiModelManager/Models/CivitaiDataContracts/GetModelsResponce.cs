using NetCivitaiModelManager.Models.CivitaiDataContracts.GetCreators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models.CivitaiDataContracts
{
    public class GetModelsResponce
    {
        [JsonPropertyName("items")]
        public List<Model> Items { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
    }
}
