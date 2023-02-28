using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public partial class LocalFile : ObservableObject
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public string? Path { get; set; }
        public string? Extension { get; set; }
        public string? ImagePath { get; set; }
        [ObservableProperty]
        private string? hash;
        [ObservableProperty]
        private bool hashRedy; 
    }
}
