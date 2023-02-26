using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CivitaiApi.CivitaiDataContracts
{
    public class Image
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("hash")]
        public string Hash { get; set; }

        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("generationProcess")]
        public string GenerationProcess { get; set; }

        [JsonPropertyName("needsReview")]
        public bool NeedsReview { get; set; }

        [JsonPropertyName("tags")]
        public List<Tag> Tags { get; set; }
    }
}
