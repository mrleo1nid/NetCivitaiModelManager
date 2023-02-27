
using CivitaiApi.CivitaiDataContracts;
using CivitaiApi.CivitaiRequestParams;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivitaiApi.Services
{
    public interface ICivitaiService
    {
        [Get("/api/v1/creators")]
        Task<BaseResponce> GetCreators(BaseRequestParams @params);

        [Get("/api/v1/models")]
        [QueryUriFormat(UriFormat.Unescaped)]
        Task<GetModelsResponce> GetModels(GetModelsParams @params);

        [Get("/api/v1/models/{id}")]
        Task<Model> GetModel(int id);

        [Get("/api/v1/model-versions/{id}")]
        Task<ModelVersion> GetModelVersion(int id);

        [Get("/api/v1/model-versions/by-hash/{hash}")]
        Task<ModelVersion> GetModelVersion(string hash);
        [Get("/api/v1/tags")]
        Task<BaseResponce> GetTags(BaseRequestParams @params);
    }
}
