using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CivitaiApi.CivitaiDataContracts
{
    public class Creator
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }
    }
}
