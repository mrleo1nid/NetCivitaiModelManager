using CivitaiApi.CivitaiRequestParams;
using CivitaiApi.Services;
using NetCivitaiModelManager.Services;
using Refit;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            LoadAutors();
            LoadModel();
            LoadModels();
            LoadVersion();
            LoadVersionHash();
            LoadTags();
        }
        private void LoadAutors()
        {
            try
            {
                var autors = _service.GetCreators(new BaseRequestParams()).Result;
            }
            catch (ValidationApiException validationException)
            {
                Log.Error(validationException, "ValidationExeption");
            }
            catch (ApiException exception)
            {
                Log.Error(exception, "ApiException");
            }
        }
        private void LoadModel()
        {
            try
            {
                var autors = _service.GetModel(4823).Result;
            }
            catch (ValidationApiException validationException)
            {
                Log.Error(validationException, "ValidationExeption");
            }
            catch (ApiException exception)
            {
                Log.Error(exception, "ApiException");
            }
        }
        private void LoadModels()
        {
            try
            {
                var autors = _service.GetModels(new GetModelsParams()).Result;
            }
            catch (ValidationApiException validationException)
            {
                Log.Error(validationException, "ValidationExeption");
            }
            catch (ApiException exception)
            {
                Log.Error(exception, "ApiException");
            }
        }
        private void LoadVersion()
        {
            try
            {
                var autors = _service.GetModelVersion(1318).Result;
            }
            catch (ValidationApiException validationException)
            {
                Log.Error(validationException, "ValidationExeption");
            }
            catch (ApiException exception)
            {
                Log.Error(exception, "ApiException");
            }
        }
        private void LoadVersionHash()
        {
            try
            {
                var autors = _service.GetModelVersion("9ABA26ABDF").Result;
            }
            catch (ValidationApiException validationException)
            {
                Log.Error(validationException, "ValidationExeption");
            }
            catch (ApiException exception)
            {
                Log.Error(exception, "ApiException");
            }
        }
        private void LoadTags()
        {
            try
            {
                var autors = _service.GetTags(new BaseRequestParams()).Result;
            }
            catch (ValidationApiException validationException)
            {
                Log.Error(validationException, "ValidationExeption");
            }
            catch (ApiException exception)
            {
                Log.Error(exception, "ApiException");
            }
        }
    }
}
