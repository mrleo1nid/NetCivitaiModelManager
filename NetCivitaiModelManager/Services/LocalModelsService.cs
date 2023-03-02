using CivitaiApi.CivitaiRequestParams;
using Microsoft.Extensions.Logging;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NetCivitaiModelManager.Extensions;

namespace NetCivitaiModelManager.Services
{
    public class LocalModelsService
    {
       
        private string[] fileFormats = new string[] { ".pt", ".ckpt", ".safetensors", ".bin", ".pth" };
        private readonly ILogger<LocalModelsService> _logger;
        private readonly ConfigService _configService;
       
        public LocalModelsService(ILogger<LocalModelsService> logger, ConfigService configService)
        {
            _logger = logger;
            _configService = configService; 
        }

        public async Task<List<LocalModel>> GetLocalModelsAsync()
        {
            var models = new List<LocalModel>();
            var folder = _configService.Config.WebUiFolderPath;
            models.AddRange(GetModelsAsync(folder, TypesEnum.Checkpoint));
            models.AddRange(GetModelsAsync(folder, TypesEnum.LORA));
            models.AddRange(GetModelsAsync(folder, TypesEnum.AestheticGradient));
            models.AddRange(GetModelsAsync(folder, TypesEnum.Hypernetwork));
            models.AddRange(GetModelsAsync(folder, TypesEnum.TextualInversion));
            models.AddRange(GetModelsAsync(folder, TypesEnum.Controlnet));
            models.AddRange(GetModelsAsync(folder, TypesEnum.Poses));
            return models;
        }

        
        private List<LocalModel> GetModelsAsync(string path, TypesEnum types)
        {
            var models = new List<LocalModel>();
            var folder = types.GetFolderByType(path);
            if(!Directory.Exists(folder)) return models;

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
            else return Path.Combine(Environment.CurrentDirectory, "Icons\\card-no-preview.png");
        }
        
    }
}
