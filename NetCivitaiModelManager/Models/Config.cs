using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace NetCivitaiModelManager.Models
{
    public class Config
    {
        
        [JsonPropertyName("CivitaiBaseUrl")]
        public string CivitaiBaseUrl { get; set; } = "";

        [JsonPropertyName("WebUiFolderPath")]
        public string WebUiFolderPath { get; set; } = "";

        [JsonPropertyName("ApiKey")]
        public string ApiKey { get; set; } = "";
    }
}