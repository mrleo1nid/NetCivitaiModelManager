using CivitaiApiWrapper.Enums;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using CivitaiApiWrapper.Extension;
using System.Linq;
using System.Runtime.Serialization;

namespace CivitaiApiWrapper.DataContracts.Requsts
{
    public class ModelsRequstParameters : BaseQueryParameters
    {
        [AliasAs("tag")]
        public string? Tag { get; set;}
        [AliasAs("username")]
        public string? Username { get; set; }
        [AliasAs("types")]
        [QueryAttribute(CollectionFormat.Multi)]
        public List<string>? TypesStr => Types.ToStringList();
        [AliasAs("sort")]
        public string? SortStr => Sort.GetEnumDescription();
        [AliasAs("period")]
        public string? PeriodStr => Period.GetEnumDescription();
        [AliasAs("rating")]
        public int? Rating { get; set; }
        [AliasAs("favorites")]
        public bool? Favorites { get; set; }
        [AliasAs("hidden")]
        public bool? Hidden { get; set; }
        [AliasAs("primaryFileOnly")]
        public bool? PrimaryFileOnly { get; set; }

        [IgnoreDataMember]
        public List<Types> Types { get; set; }
        [IgnoreDataMember]
        public Period Period { get; set; }
        [IgnoreDataMember]
        public Sort Sort { get; set; }
    }
}
