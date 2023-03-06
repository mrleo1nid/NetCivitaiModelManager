using Avalonia.Logging;
using FluentAvalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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

        [JsonPropertyName("CashFolder")]
        public string CashPath { get; set; } = "cash";
        [JsonPropertyName("CashFileName")]
        public string CashFileName { get; set; } = "blobcash";
        [JsonPropertyName("CurrentTheme")]
        public string CurrentTheme { get; set; } = FluentAvaloniaTheme.LightModeString;
    }
}
