using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class Meta
    {
        [JsonPropertyName("Size")]
        public string Size { get; set; }

        [JsonPropertyName("seed")]
        public object? Seed { get; set; }

        [JsonPropertyName("Model")]
        public string Model { get; set; }

        [JsonPropertyName("steps")]
        public int Steps { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("sampler")]
        public string Sampler { get; set; }

        [JsonPropertyName("cfgScale")]
        public double? CfgScale { get; set; }

        [JsonPropertyName("resources")]
        public List<Resource> Resources { get; set; }

        [JsonPropertyName("Model hash")]
        public string ModelHash { get; set; }

        [JsonPropertyName("negativePrompt")]
        public string NegativePrompt { get; set; }

        [JsonPropertyName("Hires steps")]
        public string HiresSteps { get; set; }

        [JsonPropertyName("Hires upscale")]
        public string HiresUpscale { get; set; }

        [JsonPropertyName("Hires upscaler")]
        public string HiresUpscaler { get; set; }

        [JsonPropertyName("Denoising strength")]
        public string DenoisingStrength { get; set; }

        [JsonPropertyName("ENSD")]
        public string ENSD { get; set; }

        [JsonPropertyName("Face restoration")]
        public string FaceRestoration { get; set; }

        [JsonPropertyName("Conditional mask weight")]
        public string ConditionalMaskWeight { get; set; }

        [JsonPropertyName("Mask blur")]
        public string MaskBlur { get; set; }

        [JsonPropertyName("Image CFG scale")]
        public string ImageCFGScale { get; set; }

        [JsonPropertyName("Clip skip")]
        public string ClipSkip { get; set; }

        [JsonPropertyName("Batch pos")]
        public string BatchPos { get; set; }

        [JsonPropertyName("Batch size")]
        public string BatchSize { get; set; }

        [JsonPropertyName("SD upscale overlap")]
        public string SDUpscaleOverlap { get; set; }

        [JsonPropertyName("SD upscale upscaler")]
        public string SDUpscaleUpscaler { get; set; }

        [JsonPropertyName("Mimic scale")]
        public string MimicScale { get; set; }

        [JsonPropertyName("Threshold percentile")]
        public string ThresholdPercentile { get; set; }

        [JsonPropertyName("Dynamic thresholding enabled")]
        public string DynamicThresholdingEnabled { get; set; }

        [JsonPropertyName("ControlNet Model")]
        public string ControlNetModel { get; set; }

        [JsonPropertyName("ControlNet Module")]
        public string ControlNetModule { get; set; }

        [JsonPropertyName("ControlNet Weight")]
        public string ControlNetWeight { get; set; }

        [JsonPropertyName("ControlNet Enabled")]
        public string ControlNetEnabled { get; set; }

        [JsonPropertyName("Hires resize")]
        public string HiresResize { get; set; }

        [JsonPropertyName("Eta")]
        public string Eta { get; set; }

        [JsonPropertyName("CFG mode")]
        public string CFGMode { get; set; }

        [JsonPropertyName("Mimic mode")]
        public string MimicMode { get; set; }

        [JsonPropertyName("CFG scale minimum")]
        public string CFGScaleMinimum { get; set; }

        [JsonPropertyName("Mimic scale minimum")]
        public string MimicScaleMinimum { get; set; }

        [JsonPropertyName("First pass size")]
        public string FirstPassSize { get; set; }
    }
}
