using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CivitaiApi.CivitaiDataContracts
{
    public class File
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sizeKB")]
        public double SizeKB { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }

        [JsonPropertyName("pickleScanResult")]
        public string PickleScanResult { get; set; }

        [JsonPropertyName("pickleScanMessage")]
        public string PickleScanMessage { get; set; }

        [JsonPropertyName("virusScanResult")]
        public string VirusScanResult { get; set; }

        [JsonPropertyName("scannedAt")]
        public DateTime ScannedAt { get; set; }

        [JsonPropertyName("hashes")]
        public Hashes Hashes { get; set; }

        [JsonPropertyName("downloadUrl")]
        public string DownloadUrl { get; set; }
    }
}
