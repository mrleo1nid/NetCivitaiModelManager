using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CivitaiApi.CivitaiDataContracts
{
    public class Metadata
    {
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("nextPage")]
        public string NextPage { get; set; }
    }
}
