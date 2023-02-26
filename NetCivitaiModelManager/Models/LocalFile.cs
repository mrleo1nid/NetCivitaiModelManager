using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public class LocalFile
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? Path { get; set; }
        public string? Extension { get; set; }
        public string? ImagePath { get; set; }
        public string? Hash { get; set; }
    }
}
