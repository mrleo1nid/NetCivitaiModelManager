using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models.CivitaiDataContracts
{
    public class Meta
    {
        [JsonPropertyName("Size")]
        public string Size { get; set; }

        [JsonPropertyName("seed")]
        public Int64 Seed { get; set; }

        [JsonPropertyName("steps")]
        public int Steps { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("sampler")]
        public string Sampler { get; set; }

        [JsonPropertyName("cfgScale")]
        public double CfgScale { get; set; }

        [JsonPropertyName("Model hash")]
        public string ModelHash { get; set; }

        [JsonPropertyName("negativePrompt")]
        public string NegativePrompt { get; set; }

        [JsonPropertyName("Batch pos")]
        public string BatchPos { get; set; }

        [JsonPropertyName("Batch size")]
        public string BatchSize { get; set; }

        [JsonPropertyName("ENSD")]
        public string ENSD { get; set; }

        [JsonPropertyName("Model")]
        public string Model { get; set; }
    }
}
