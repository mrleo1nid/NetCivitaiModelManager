using NetCivitaiModelManager.Services;
using Serilog;
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
                var autors = _service.GetCreators(new Models.CivitaiRequestParams.BaseRequestParams()).Result;
            }
            catch(Exception ex) { Log.Error(ex, "Api error"); }
        }
        private void LoadModel()
        {
            try
            {
                var autors = _service.GetModel(4823).Result;
            }
            catch (Exception ex) { Log.Error(ex, "Api error"); }
        }
        private void LoadModels()
        {
            try
            {
                var autors = _service.GetModels(new Models.CivitaiRequestParams.GetModelsParams()).Result;
            }
            catch (Exception ex) { Log.Error(ex, "Api error"); }
        }
        private void LoadVersion()
        {
            try
            {
                var autors = _service.GetModelVersion(1318).Result;
            }
            catch (Exception ex) { Log.Error(ex, "Api error"); }
        }
        private void LoadVersionHash()
        {
            try
            {
                var autors = _service.GetModelVersion("9ABA26ABDF").Result;
            }
            catch (Exception ex) { Log.Error(ex, "Api error"); }
        }
        private void LoadTags()
        {
            try
            {
                var autors = _service.GetTags(new Models.CivitaiRequestParams.BaseRequestParams()).Result;
            }
            catch (Exception ex) { Log.Error(ex, "Api error"); }
        }
    }
}
