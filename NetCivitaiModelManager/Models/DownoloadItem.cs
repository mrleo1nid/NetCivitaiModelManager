using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public class DownoloadItem
    {
        public Guid Id { get; private set; }
        public string Url { get; private set; }
        public string TargetPath { get; private set; }
        public string BufferPath { get; private set; }
        public DownoloadItem(string url, string targetpath, string bufferpath) 
        {
            Id = Guid.NewGuid();
            Url = url;
            TargetPath = targetpath;
            BufferPath = bufferpath;
        }
    }
}
