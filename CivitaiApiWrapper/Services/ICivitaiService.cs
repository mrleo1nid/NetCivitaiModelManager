using CivitaiApiWrapper.DataContracts;
using CivitaiApiWrapper.DataContracts.Requsts;
using Refit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CivitaiApiWrapper.Services
{
    public interface ICivitaiService
    {
        [Get("/api/v1/creators")]
        Task<BaseMetadataResponce<Creator>> GetCreators(BaseQueryParameters query);

        [Get("/api/v1/models")]
        [QueryUriFormat(UriFormat.Unescaped)]
        Task<BaseMetadataResponce<Model>> GetModels(ModelsRequstParameters query);
        
        [Get("/api/v1/models/{id}")]
        Task<Model> GetModel(int id);
        
        [Get("/api/v1/model-versions/{id}")]
        Task<ModelVersion> GetModelVersion(int id);
        
        [Get("/api/v1/model-versions/by-hash/{hash}")]
        Task<ModelVersion> GetModelVersion(string hash);
        [Get("/api/v1/tags")]
        Task<BaseMetadataResponce<Tag>> GetTags(BaseQueryParameters query);
    }
}
