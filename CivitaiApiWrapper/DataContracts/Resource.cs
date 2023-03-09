using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class Resource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("weight")]
        public double? Weight { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }
    }
}
