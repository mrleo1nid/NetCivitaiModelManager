using CivitaiApiWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCivitaiModelManagerCore.Models
{
    public class LocalFile
    {
        public string FullPath { get; set; }
        public Types Type { get; set; }
        public long Size { get; set; }
        public string Hash { get; set; }
    }
}
