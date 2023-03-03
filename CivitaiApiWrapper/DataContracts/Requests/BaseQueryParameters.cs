using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CivitaiApiWrapper.DataContracts.Requsts
{
    public class BaseQueryParameters
    {
        [AliasAs("limit")]
        public int? Limit { get; set; }
        [AliasAs("page")]
        public int? Page { get; set; }
        [AliasAs("query")]
        public int? Query { get; set; }
    }
}
