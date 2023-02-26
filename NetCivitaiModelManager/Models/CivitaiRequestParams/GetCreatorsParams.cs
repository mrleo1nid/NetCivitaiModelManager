﻿using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models.CivitaiRequestParams
{
    public class GetCreatorsParams
    {
        [AliasAs("limit ")]
        public int Limit { get; set; }
        [AliasAs("page")]
        public int Page { get; set; }
        [AliasAs("query")]
        public string Query { get; set; }
    }
}
