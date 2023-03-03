using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CivitaiApiWrapper.DataContracts
{
    public class Stats
    {
        [JsonPropertyName("downloadCount")]
        public int DownloadCount { get; set; }

        [JsonPropertyName("favoriteCount")]
        public int FavoriteCount { get; set; }

        [JsonPropertyName("commentCount")]
        public int CommentCount { get; set; }

        [JsonPropertyName("ratingCount")]
        public int RatingCount { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }
    }
}
