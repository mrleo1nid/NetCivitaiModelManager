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
        public string Type { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }

        [JsonPropertyName("poi")]
        public bool Poi { get; set; }
    }
}
