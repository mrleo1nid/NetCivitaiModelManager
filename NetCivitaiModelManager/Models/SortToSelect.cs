using CivitaiApi.CivitaiRequestParams;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public class SortToSelect
    {
        public string Name { get; set; }
        public SortEnum Sort { get; set; }
        public SortToSelect(SortEnum type)
        {
            Sort = type;
            Name = ((SortEnum)type).AsString(EnumFormat.Description);
        }
    }
}
