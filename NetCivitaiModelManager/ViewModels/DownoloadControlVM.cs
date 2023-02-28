using NetCivitaiModelManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public class DownoloadControlVM : BaseVM
    {
        public FileDownoloadService FileDownoloadService { get; set; }
        public DownoloadControlVM(FileDownoloadService fileDownoloadService)
        {
            FileDownoloadService = fileDownoloadService;
            FileDownoloadService.AddAndStart("https://imagecache.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/2d37f24e-f9fd-4900-29b7-e9d9548ce100/width=450", "D:\backup\\1.png").Wait();
            FileDownoloadService.AddAndStart("https://imagecache.civitai.com/xG1nkqKTMzGDvpLrqFT7WA/2d37f24e-f9fd-4900-29b7-e9d9548ce100/width=450", "D:\backup\\2.png").Wait();
            FileDownoloadService.AddAndStart("https://civitai.com/api/download/models/4048?type=Pruned%20Model&format=SafeTensor", "D:\backup\\3.safetensor").Wait();
        }
    }
}
