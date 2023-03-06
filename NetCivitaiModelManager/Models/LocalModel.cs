using CivitaiApiWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public class LocalModel
    {
        public string Name { get; set; }
        public string LocalFilePath { get; set; }
        public string? LocalImagePath { get; set; }
        public Types Type { get; set; }
    }
}
