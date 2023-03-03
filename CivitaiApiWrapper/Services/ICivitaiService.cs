using CivitaiApiWrapper.DataContracts;
using CivitaiApiWrapper.DataContracts.Requsts;
using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace CivitaiApiWrapper.Services
{
    public interface ICivitaiService
    {
        [Get("/api/v1/creators")]
        Task<BaseMetadataResponce<Creator>> GetCreators(BaseQueryParameters query,
            CancellationToken cancellationToken = default(CancellationToken));

        [Get("/api/v1/models")]
        Task<BaseMetadataResponce<Model>> GetModels(ModelsRequstParameters query,
            CancellationToken cancellationToken = default(CancellationToken));
        
        [Get("/api/v1/models/{id}")]
        Task<Model> GetModel(int id,
            CancellationToken cancellationToken = default(CancellationToken));
        
        [Get("/api/v1/model-versions/{id}")]
        Task<ModelVersion> GetModelVersion(int id,
            CancellationToken cancellationToken = default(CancellationToken));

        
        [Get("/api/v1/model-versions/by-hash/{hash}")]
        Task<ModelVersion> GetModelVersion(string hash,
            CancellationToken cancellationToken = default(CancellationToken));

        [Get("/api/v1/tags")]
        Task<BaseMetadataResponce<Tag>> GetTags(BaseQueryParameters query,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
