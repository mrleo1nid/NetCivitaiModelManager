using Avalonia.Logging;
using FluentAvalonia.Styling;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public class Config : ReactiveObject
    {
        [JsonPropertyName("CivitaiBaseUrl")]
        [Reactive] public string CivitaiBaseUrl { get; set; } = "https://civitai.com/";

        [JsonPropertyName("WebUiFolderPath")]
        [Reactive] public string WebUiFolderPath { get; set; } = "D:\\stable-diffusion-webui";

        [JsonPropertyName("ApiKey")]
        [Reactive] public string ApiKey { get; set; } = "";

        [JsonPropertyName("CashFolder")]
        [Reactive] public string CashPath { get; set; } = "cash";
        [JsonPropertyName("CashFileName")]
        [Reactive] public string CashFileName { get; set; } = "blobcash";
        [JsonPropertyName("CurrentTheme")]
        [Reactive] public string CurrentTheme { get; set; } = FluentAvaloniaTheme.LightModeString;
        [JsonPropertyName("ExternalModelInPage")]
        [Reactive] public int ExternalModelInPage { get; set; } = 24;
    }
}
