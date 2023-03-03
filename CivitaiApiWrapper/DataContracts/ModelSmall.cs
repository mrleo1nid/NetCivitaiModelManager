using CivitaiApiWrapper.Enums;
using CivitaiApiWrapper.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class ModelSmall
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string TypeStr { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }

        [JsonPropertyName("poi")]
        public bool Poi { get; set; }
        [JsonIgnore]
        public Types Type => TypeStr.ToEnum<Types>();
    }
}
