using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class Model
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("poi")]
        public bool Poi { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }

        [JsonPropertyName("allowNoCredit")]
        public bool AllowNoCredit { get; set; }

        [JsonPropertyName("allowCommercialUse")]
        public string AllowCommercialUse { get; set; }

        [JsonPropertyName("allowDerivatives")]
        public bool AllowDerivatives { get; set; }

        [JsonPropertyName("allowDifferentLicense")]
        public bool AllowDifferentLicense { get; set; }

        [JsonPropertyName("stats")]
        public Stats Stats { get; set; }

        [JsonPropertyName("creator")]
        public Creator Creator { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("modelVersions")]
        public List<ModelVersion> ModelVersions { get; set; }
    }
}
