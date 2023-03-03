using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class Tag
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("modelCount")]
        public int ModelCount { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
