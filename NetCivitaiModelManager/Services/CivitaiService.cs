using CivitaiApi.CivitaiDataContracts;
using CivitaiApi.CivitaiRequestParams;
using CivitaiApi.Services;
using Microsoft.Extensions.Logging;
using Refit;
using Serilog;
using System.Threading.Tasks;
using System.Windows;

namespace NetCivitaiModelManager.Services
{
    public class CivitaiService
    {
        ICivitaiService _service;
        ILogger<CivitaiService> _logger;
        public CivitaiService(ICivitaiService service, ILogger<CivitaiService> logger)
        {
            _service = service;
            _logger = logger;
        }
        public async Task<BaseResponce?> GetCreatorsAsync(BaseRequestParams requestParams)
        {
            try
            {
                return await _service.GetCreators(requestParams);
            }
            catch (ValidationApiException validationException)
            {
                ValidationEx(validationException);
                return null;
            }
            catch (ApiException exception)
            {
                ApiEx(exception);
                return null;
            }
        }
        public async Task<Model?> GetModelAsync(int id)
        {
            try
            {
                return await _service.GetModel(id);
            }
            catch (ValidationApiException validationException)
            {
                ValidationEx(validationException);
                return null;
            }
            catch (ApiException exception)
            {
                ApiEx(exception);
                return null;
            }
        }
        public async Task<GetModelsResponce?> GetModelsAsync(GetModelsParams getModelsParams)
        {
            try
            {
                return await _service.GetModels(getModelsParams);
            }
            catch (ValidationApiException validationException)
            {
                ValidationEx(validationException);
                return null;
            }
            catch (ApiException exception)
            {
                ApiEx(exception);
                return null;
            }
        }
        public async Task<ModelVersion?> GetVersionAsync(int id)
        {
            try
            {
                return await _service.GetModelVersion(id);
            }
            catch (ValidationApiException validationException)
            {
                ValidationEx(validationException);
                return null;
            }
            catch (ApiException exception)
            {
                ApiEx(exception);
                return null;
            }
        }
        public async Task<ModelVersion?> GetVersionAsync(string hash)
        {
            try
            {
                return await _service.GetModelVersion(hash);
            }
            catch (ValidationApiException validationException)
            {
                ValidationEx(validationException);
                return null;
            }
            catch (ApiException exception)
            {
                ApiEx(exception);
                return null;
            }
        }
        public async Task<BaseResponce?> GetTagsAsync(BaseRequestParams requestParams)
        {
            try
            {
                return await _service.GetTags(requestParams);
            }
            catch (ValidationApiException validationException)
            {
                ValidationEx(validationException);
                return null;
            }
            catch (ApiException exception)
            {
                ApiEx(exception);
                return null;
            }
        }
        private void ValidationEx(ValidationApiException validationException)
        {
            _logger.LogError(validationException, "ValidationExeption");
            MessageError(validationException.Message, "ValidationExeption");
        }
        private void ApiEx(ApiException exception)
        {
            _logger.LogError(exception, "ApiException");
            MessageError(exception.Message, "ApiException");
        }
        private void MessageError(string message, string type)
        {
            MessageBox.Show($"Произошла ошибка Type: {type}, Message: {message}. Подробности смотри в логе.", "Ошибка!", MessageBoxButton.OK);
        }
    }
}
