using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class Hashes
    {
        [JsonPropertyName("SHA256")]
        public string SHA256 { get; set; }

        [JsonPropertyName("AutoV1")]
        public string AutoV1 { get; set; }

        [JsonPropertyName("AutoV2")]
        public string AutoV2 { get; set; }

        [JsonPropertyName("BLAKE3")]
        public string BLAKE3 { get; set; }

        [JsonPropertyName("CRC32")]
        public string CRC32 { get; set; }
    }
}
