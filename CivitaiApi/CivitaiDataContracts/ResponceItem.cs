using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CivitaiApi.CivitaiDataContracts
{
    public class ResponceItem
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("modelCount")]
        public int ModelCount { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
