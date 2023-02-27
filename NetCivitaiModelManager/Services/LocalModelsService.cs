using CivitaiApi.CivitaiRequestParams;
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
using static System.Net.Mime.MediaTypeNames;

namespace NetCivitaiModelManager.Services
{
    public class LocalModelsService
    {
        private string[] fileFormats = new string[] { ".pt", ".ckpt", ".safetensors", ".bin" };
        private readonly ILogger<LocalModelsService> _logger;
        private readonly ConfigService _configService;
        private Dictionary<string, string> _calculatedHash;
        public LocalModelsService(ILogger<LocalModelsService> logger, ConfigService configService)
        {
            _logger = logger;
            _configService = configService; 
            _calculatedHash = new Dictionary<string, string>();
        }

        public async Task<List<LocalModel>> GetLocalModelsAsync()
        {
            var models = new List<LocalModel>();
            var folder = _configService.Config.WebUiFolderPath;
            models.AddRange(await GetModelsAsync(folder, TypesEnum.Checkpoint));
            models.AddRange(await GetModelsAsync(folder, TypesEnum.LORA));
            models.AddRange(await GetModelsAsync(folder, TypesEnum.AestheticGradient));
            models.AddRange(await GetModelsAsync(folder, TypesEnum.Hypernetwork));
            models.AddRange(await GetModelsAsync(folder, TypesEnum.TextualInversion));
            return models;
        }
        public async Task CalculateHash(List<LocalModel> localModels)
        {
            foreach(var localModel in localModels)
            {
                if (!string.IsNullOrEmpty(localModel.LocalFile?.Hash))
                    continue;
               
                var hash = string.Empty;
                if(!string.IsNullOrEmpty(_calculatedHash[localModel.LocalFile.FullName]))
                    hash = _calculatedHash[localModel.LocalFile.FullName];
                else
                {
                    var algo = SHA256.Create();
                    using (var stream = File.OpenRead(localModel.LocalFile.FullName))
                        hash = await stream.GetHashAsync(algo);
                    _calculatedHash[localModel.LocalFile.FullName] = hash;  
                }
                localModel.LocalFile.Hash = hash;   
            }
        }
        private async Task<List<LocalModel>> GetModelsAsync(string path, TypesEnum types)
        {
            var models = new List<LocalModel>();
            var folder = GetFolderByType(path,types);
            var files = Directory.GetFiles(folder).Where(x=> fileFormats.Contains(Path.GetExtension(x)));
            
            foreach (var file in files)
            {
                var localfile = new LocalFile();
                localfile.FullName = file;
                localfile.Name = Path.GetFileNameWithoutExtension(file);
                localfile.Extension = Path.GetExtension(file);
                localfile.ImagePath = GetImage(file, localfile.Name);
                var localmodel = new LocalModel()
                {
                    LocalFile = localfile,
                    DisplayImage = localfile.ImagePath,
                    DisplayName = localfile.Name,
                    Type = types
                };
                models.Add(localmodel);
            }
            return models;
        }
        private string GetImage(string filepath, string filename)
        {
            var folder = Path.GetDirectoryName(filepath);
            var imagepath = Path.Combine(folder, filename)+ ".preview.png";
            if(File.Exists(imagepath)) return imagepath;
            else return Path.Combine(Environment.CurrentDirectory, "Icons\\icon-notfound.png");
        }
        private string GetFolderByType(string basepath,TypesEnum typesEnum)
        {
            var result = string.Empty;
            switch (typesEnum)
            {
                case TypesEnum.LORA:
                    result = "models\\Lora";
                    break;
                case TypesEnum.TextualInversion:
                    result = "embeddings";
                    break;
                case TypesEnum.Hypernetwork:
                    result = "models\\hypernetworks";
                    break;
                case TypesEnum.Checkpoint:
                    result = "models\\Stable-diffusion";
                    break;
                case TypesEnum.AestheticGradient:
                    result = "extensions\\stable-diffusion-webui-aesthetic-gradients\\aesthetic_embeddings";
                    break;
            }
            return Path.Combine(basepath,result);
        }

        internal Action CalculateHash(object alllocalModels)
        {
            throw new NotImplementedException();
        }
    }
}
