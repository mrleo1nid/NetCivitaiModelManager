using NetCivitaiModelManager.Models.CivitaiDataContracts.GetCreators;
using NetCivitaiModelManager.Models.CivitaiRequestParams;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public interface ICivitaiService
    {
        [Get("/api/v1/creators")]
        Task<GetCreatorsResponce> GetCreators(GetCreatorsParams @params);
    }
}
