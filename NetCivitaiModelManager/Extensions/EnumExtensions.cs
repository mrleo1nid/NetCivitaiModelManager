using CivitaiApi.CivitaiRequestParams;
using EnumsNET;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extensions
{
    public static class EnumExtensions
    {
        
        public static string GetEnumDescription(this TypesEnum Enum)
        {
            return ((TypesEnum)Enum).AsString(EnumFormat.Description);
        }
        public static string GetEnumDescription(this SortEnum Enum)
        {
            return ((SortEnum)Enum).AsString(EnumFormat.Description);
        }
        public static string GetEnumDescription(this PeriodEnum Enum)
        {
            return ((PeriodEnum)Enum).AsString(EnumFormat.Description);
        }
    }
}
