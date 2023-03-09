using CivitaiApiWrapper.DataContracts;
using CivitaiApiWrapper.DataContracts.Requsts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using System.Collections;
using Splat;
using Microsoft.Extensions.Logging;

namespace CivitaiApiWrapper.Services
{
    public class PoliCivitaiService : ICivitaiService
    {
        private readonly ICivitaiService _service;
        private readonly int _retryCount;
        private readonly ILogger<PoliCivitaiService> _logger;
        public PoliCivitaiService(ICivitaiService service)
        {
            _service = service;
            _retryCount = 3; 
        }

        public async Task<BaseMetadataResponce<Creator>> GetCreators(BaseQueryParameters query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await MakeRequest(ct => _service.GetCreators(query), cancellationToken);
        }

        public async Task<Model> GetModel(int id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await MakeRequest(ct => _service.GetModel(id), cancellationToken);
        }

        public async Task<BaseMetadataResponce<Model>> GetModels(ModelsRequstParameters query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await MakeRequest(ct => _service.GetModels(query), cancellationToken);
        }

        public async Task<ModelVersion> GetModelVersion(int id,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await MakeRequest(ct => _service.GetModelVersion(id), cancellationToken);
        }

        public async Task<ModelVersion> GetModelVersion(string hash,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await MakeRequest(ct => _service.GetModelVersion(hash), cancellationToken);
        }

        public async Task<BaseMetadataResponce<Tag>> GetTags(BaseQueryParameters query,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await MakeRequest(ct => _service.GetTags(query), cancellationToken);
        }

        private async Task<T> MakeRequest<T>(Func<CancellationToken, Task<T>> loadingFunction, CancellationToken cancellationToken)
        {
            Exception exception = null;
            var result = default(T);
            try
            {
                result = await Policy.Handle<WebException>().Or<HttpRequestException>()
                    .WaitAndRetryAsync(_retryCount, i => TimeSpan.FromMilliseconds(300), (ex, span) => exception = ex)
                    .ExecuteAsync(loadingFunction, cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }
            //TODO: Обработать исключения или передать их дальше            
            return result;
        }
    }
}
