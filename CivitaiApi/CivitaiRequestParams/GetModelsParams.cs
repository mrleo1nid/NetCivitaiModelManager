using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivitaiApi.CivitaiRequestParams
{
    public class GetModelsParams : BaseRequestParams
    {
        [AliasAs("tag  ")]
        public string? Tag { get; set; }
        [AliasAs("username  ")]
        public string? Username { get; set; }
        [AliasAs("types")]
        public List<string>? Types { get; set; }
        [AliasAs("sort")]
        public SortEnum? Sort { get; set; }
        [AliasAs("period")]
        public PeriodEnum? Period { get; set; }
        [AliasAs("rating")]
        public int? Raiting { get; set; }
        [AliasAs("favorites")]
        public bool? Favorites { get; set; }
        [AliasAs("hidden")]
        public bool? Hidden { get; set; }
    }
}
