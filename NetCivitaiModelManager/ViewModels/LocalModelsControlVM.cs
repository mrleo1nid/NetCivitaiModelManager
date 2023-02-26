using NetCivitaiModelManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.ViewModels
{
    public class LocalModelsControlVM : BaseVM
    {
        private ICivitaiService _service;
        public LocalModelsControlVM(ICivitaiService civitaiService)
        {
            _service = civitaiService;
            var autors = _service.GetCreators(new Models.CivitaiRequestParams.GetCreatorsParams());
            autors.Wait();
        }

    }
}
