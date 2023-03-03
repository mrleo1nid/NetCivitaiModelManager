using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class Creator
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("modelCount")]
        public int ModelCount { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }
    }
}
