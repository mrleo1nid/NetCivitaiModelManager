using CivitaiApiWrapper.DataContracts;
using CivitaiApiWrapper.DataContracts.Requsts;
using CivitaiApiWrapper.Enums;
using CivitaiApiWrapper.Services;
using DynamicData;
using NetCivitaiModelManager.Models;
using NLog;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Refit;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class ExternalModelsService : ReactiveObject
    {
        private IFullLogger _logger;
        private readonly ConfigService _configService;
        private readonly SourceList<Model> _externalModels;
        public IObservable<IChangeSet<Model>> Connect() => _externalModels.Connect();
        private ICivitaiService _civitaiService;
        private PoliCivitaiService _poliCivitaiService;
        [Reactive] public int TotalPages { get; set; }
        [Reactive]public int TotalItems { get; set; }
        [Reactive] public bool InProgress { get; set; }
        public ExternalModelsService(ConfigService configService, ILogManager logManager)
        {
            _logger = logManager.GetLogger<ExternalModelsService>();
            _externalModels = new SourceList<Model>();
            _configService = configService;
            CreateServices();
            TotalItems = 0;
            TotalPages = 999;
            InProgress = false;
        }

        public async Task AddRequest(ModelsRequstParameters modelsRequstParameters)
        {
            if(!InProgress)
            {
                InProgress = true;
                var responce = await _poliCivitaiService.GetModels(modelsRequstParameters);
                if (responce != null)
                {
                    _externalModels.Clear();
                    _externalModels.AddRange(responce.Items);
                    TotalPages = responce.Metadata.TotalPages;
                    TotalItems = responce.Metadata.TotalItems;
                }
                InProgress = false;
            }
        }

        private void CreateServices()
        {
            _civitaiService = RestService.For<ICivitaiService>(_configService.Config.CivitaiBaseUrl);
            _poliCivitaiService = new PoliCivitaiService(_civitaiService, _logger, 3);
        }
    }
}
