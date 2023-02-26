using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models.CivitaiDataContracts.GetCreators
{
    public class GetCreatorsResponce
    {
        [JsonPropertyName("items")]
        public List<ResponceItem> Items { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
    }
}
