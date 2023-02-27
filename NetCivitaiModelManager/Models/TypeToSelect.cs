using CivitaiApi.CivitaiRequestParams;
using EnumsNET;
using NetCivitaiModelManager.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NetCivitaiModelManager.Models
{
    public class TypeToSelect
    {
        public string Name { get; set; }
        public TypesEnum Type { get; set; }
        public TypeToSelect(TypesEnum type)
        {
            Type = type;
            Name = type.GetEnumDescription();
        }
    }
}
