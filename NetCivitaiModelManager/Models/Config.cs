using CommunityToolkit.Mvvm.ComponentModel;
using Serilog.Events;
using System.Text.Json.Serialization;

namespace NetCivitaiModelManager.Models
{
    public class Config
    {
        
        [JsonPropertyName("CivitaiBaseUrl")]
        public string CivitaiBaseUrl { get; set; } = "https://civitai.com/";

        [JsonPropertyName("WebUiFolderPath")]
        public string WebUiFolderPath { get; set; } = "D:\\stable-diffusion-webui";

        [JsonPropertyName("ApiKey")]
        public string ApiKey { get; set; } = "";

        [JsonPropertyName("LogLevel")]
        public LogEventLevel LogLevel { get; set; } = LogEventLevel.Debug;
    }
}