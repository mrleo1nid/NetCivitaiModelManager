﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CivitaiApi.CivitaiDataContracts
{
    public class Model
    {
        [JsonIgnore]
        public string? DisplayImage { get { return ModelVersions.OrderByDescending(x => x.UpdatedAt).FirstOrDefault()?.Images.FirstOrDefault()?.Url; } }

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

        [JsonPropertyName("creator")]
        public Creator Creator { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("modelVersions")]
        public List<ModelVersion> ModelVersions { get; set; }
    }
}
