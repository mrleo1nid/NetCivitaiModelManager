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
            Task.Factory.StartNew(LoadFromCash);
        }
        public async void LoadFromCash()
        {
            await FileDownoloadService.LoadDownoloadsFromCash();
        }
    }
}
