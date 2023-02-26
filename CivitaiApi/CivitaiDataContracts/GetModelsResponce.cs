using System.Text.Json.Serialization;

namespace CivitaiApi.CivitaiDataContracts
{
    public class GetModelsResponce
    {
        [JsonPropertyName("items")]
        public List<Model> Items { get; set; }

        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
    }
}
