using CivitaiApiWrapper.Enums;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using CivitaiApiWrapper.Extension;
using System.Linq;

namespace CivitaiApiWrapper.DataContracts.Requsts
{
    public class ModelsRequstParameters : BaseQueryParameters
    {
        [JsonPropertyName("tag")]
        public string? Tag { get; set;}
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("types")]
        [QueryAttribute(CollectionFormat.Multi)]
        public List<string>? TypesStr => Types.ToStringList();
        [JsonPropertyName("sort")]
        public string? SortStr => Sort.GetEnumDescription();
        [JsonPropertyName("period")]
        public string? PeriodStr => Period.GetEnumDescription();
        [JsonPropertyName("rating")]
        public int? Rating { get; set; }
        [JsonPropertyName("favorites")]
        public bool? Favorites { get; set; }
        [JsonPropertyName("hidden")]
        public bool? Hidden { get; set; }
        [JsonPropertyName("primaryFileOnly")]
        public bool? PrimaryFileOnly { get; set; }

        [JsonIgnore]
        public List<Types> Types { get; set; }
        [JsonIgnore]
        public Period Period { get; set; }
        [JsonIgnore]
        public Sort Sort { get; set; }
    }
}
