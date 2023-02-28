using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extensions
{
    public static class ListExtensions
    {
        public static string? GetRowToRequest(this List<TypeToSelect> typeToSelects)
        {
            var types = typeToSelects.Select(x => x.Name.Replace(" ", "")).ToList();
            var type = String.Join("&types=", types);
            if (string.IsNullOrEmpty(type)) type = null;
            return type;
        }
    }
}
