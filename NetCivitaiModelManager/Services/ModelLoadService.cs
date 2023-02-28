using Microsoft.Extensions.Logging;
using NetCivitaiModelManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public partial class ModelLoadService : BaseQuequeService
    {
        public List<LocalModel> Quque { get; private set; } = new List<LocalModel>();
        private LocalModel? currentmodel;
        private ILogger<ModelLoadService> _logger;
        private CivitaiService _service;
        public ModelLoadService(ILogger<ModelLoadService> logger, CivitaiService civitaiService)
        {
            _logger = logger;
            _service = civitaiService;
        }
        public void AddToQuque(LocalModel model)
        {
            if (!Quque.Contains(model))
                Quque.Add(model);
        }
        public void Start()
        {
            if (!IsStarted)
            {
                IsStarted = true;
                QuqueCount = Quque.Count;
                Task.Factory.StartNew(RefreshModel);
            }
        }
        private async Task RefreshModel()
        {
            if (currentmodel == null)
            {
                if (Quque.Any())
                {
                    currentmodel = Quque.FirstOrDefault();
                    CurrentName = currentmodel.DisplayName;
                    await _service.LoadModelToLocal(currentmodel);
                    if(currentmodel.ExternalModel != null)
                         currentmodel.ModelFound = true;
                    Quque.Remove(currentmodel);
                    currentmodel = null;
                    QuqueCount = Quque.Count;
                    await RefreshModel();
                }
                else
                {
                    if (IsStarted)
                        IsStarted = false;
                }
            }
        }
       
    }
}
