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
            Task.Factory.StartNew(Test);
        }
        public async void Test()
        {
          await FileDownoloadService
                .AddAndStart("https://civitai.com/api/download/models/4048?type=Pruned%20Model&format=SafeTensor", "D:\\backup\\protogenX34Photorealism_1.safetensors");
            await FileDownoloadService
               .AddAndStart("https://civitai.com/api/download/models/4048?type=Pruned%20Model&format=SafeTensor", "D:\\backup\\protogenX34Photorealism_2.safetensors");
            await FileDownoloadService
               .AddAndStart("https://civitai.com/api/download/models/4048?type=Pruned%20Model&format=SafeTensor", "D:\\backup\\protogenX34Photorealism_3.safetensors");
        }
    }
}
