using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts.Requsts
{
    public class ModelsRequstParameters : BaseQueryParameters
    {
        [JsonPropertyName("tag")]
        public string? Tag { get; set;}
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        [JsonPropertyName("types")]
        [QueryAttribute(CollectionFormat.Multi)]
        public List<string> Types { get; set; }
        [JsonPropertyName("sort")]
        public string Sort { get; set; }
        [JsonPropertyName("period")]
        public string Period { get; set; }
        [JsonPropertyName("rating")]
        public int? Rating { get; set; }
        [JsonPropertyName("favorites")]
        public bool Favorites { get; set; }
        [JsonPropertyName("hidden")]
        public bool Hidden { get; set; }
        [JsonPropertyName("primaryFileOnly")]
        public bool PrimaryFileOnly { get; set; }
    }
}
