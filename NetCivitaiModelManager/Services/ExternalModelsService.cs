using CivitaiApiWrapper.DataContracts;
using CivitaiApiWrapper.Enums;
using CivitaiApiWrapper.Services;
using DynamicData;
using NetCivitaiModelManager.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class ExternalModelsService
    {
        private readonly ConfigService _configService;
        private readonly SourceList<Model> _externalModels;
        public IObservable<IChangeSet<Model>> Connect() => _externalModels.Connect();
        private ICivitaiService _civitaiService;
        private PoliCivitaiService _poliCivitaiService;
        public ExternalModelsService(ConfigService configService)
        {
            _configService = configService;
            CreateServices();
        }
        private void CreateServices()
        {
            _civitaiService = RestService.For<ICivitaiService>(_configService.Config.CivitaiBaseUrl);
            _poliCivitaiService = new PoliCivitaiService(_civitaiService);
        }
    }
}
