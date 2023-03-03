using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class BaseMetadataResponce<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; }
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
    }
}
