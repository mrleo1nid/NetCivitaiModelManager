using Microsoft.Extensions.Logging;
using NetCivitaiModelManager.Extension;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class HashService
    {
        public delegate void HashCompleteHandler(LocalModel model);
        public event HashCompleteHandler? NotifyHashComplete;
        private Dictionary<string, string> _calculatedHash;

        public List<LocalModel> Quque { get; private set; } = new List<LocalModel>();
        public LocalModel? CurrentModel { get; private set; }
        public bool IsStarted { get; private set; }
        private ILogger<HashService> _logger;
        public HashService(ILogger<HashService> logger)
        {
            _logger = logger;
            IsStarted = false;
            _calculatedHash = new Dictionary<string, string>();
        }
        public void AddToQuque(LocalModel localModel)
        {
            if(!Quque.Contains(localModel))
                Quque.Add(localModel);
        }
        public void Start()
        {
            if(!IsStarted)
            {
                IsStarted = true;
                RefreshModel();
            }
        }
        private void RefreshModel()
        {
            if (CurrentModel == null)
            {
                if (Quque.Any())
                {
                    CurrentModel = Quque.FirstOrDefault();
                    ProcessCurentModel();
                }
            }
        }
        private void ProcessCurentModel()
        {
            Task.Factory.StartNew(CalculateModelHash);
        }
        public async Task CalculateModelHash()
        {
            if (!string.IsNullOrEmpty(CurrentModel.LocalFile?.Hash))
                return;

            var hash = string.Empty;
            var identifier = GetFileIdentifier(CurrentModel.LocalFile.FullName);
                var algo = SHA256.Create();
                using (var stream = File.OpenRead(CurrentModel.LocalFile.FullName))
                    hash = await stream.GetHashAsync(algo);
            CurrentModel.LocalFile.Hash = hash;
            NotifyHashComplete?.Invoke(CurrentModel);
            RefreshModel();
        }
        private string GetFileIdentifier(string filepath)
        {
            FileInfo file = new FileInfo(filepath);
            return $"{filepath}_{file.Length}_{file.LastWriteTime.ToString("g")}";
        }
    }
}
