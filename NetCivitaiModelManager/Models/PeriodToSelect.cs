using CivitaiApi.CivitaiRequestParams;
using EnumsNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public class PeriodToSelect
    {
        public string Name { get; set; }
        public PeriodEnum Period { get; set; }
        public PeriodToSelect(PeriodEnum type)
        {
            Period = type;
            Name = ((PeriodEnum)type).AsString(EnumFormat.Description);
        }
    }
}
