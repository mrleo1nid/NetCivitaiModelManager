using CivitaiApiWrapper.Enums;
using DynamicData;
using NetCivitaiModelManager.Extensions;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class LocalModelsService
    {
        private readonly ConfigService _configService;
        private string[] fileFormats = new string[] { ".pt", ".ckpt", ".safetensors", ".bin", ".pth" };
        private readonly List<Types> _loadedTypes = new List<Types>() { Types.Checkpoint, Types.TextualInversion,
            Types.Hypernetwork, Types.AestheticGradient, Types.LORA, Types.Controlnet};

        private bool _iterationComplete = true;
        private readonly SourceList<LocalModel> _localModels;


        public bool IsEnabled { get; set; }
        public IObservable<IChangeSet<LocalModel>> Connect() => _localModels.Connect();

        public LocalModelsService(ConfigService configService)
        {
            _configService = configService;
            _localModels = new SourceList<LocalModel>(); 
            Start();  
        }
        public void Start()
        {
            IsEnabled = true;
            Task.Factory.StartNew(() => Work());
        }
        public async Task Work()
        {
            while (IsEnabled)
            {
                if (_iterationComplete)
                {
                    _iterationComplete = false;
                    foreach (var type in _loadedTypes) { LoadFromFolderByType(type); }
                    _iterationComplete = true;
                }
                await Task.Delay(5000);
            }
        }
       
        private void LoadFromFolderByType(Types type)
        {
            var folder = type.GetFolderByType(_configService.Config.WebUiFolderPath);
            var files = Directory.GetFiles(folder).Where(x=> fileFormats.Contains(Path.GetExtension(x)));
            RemoveNotExists(files, type);
            foreach (var file in files)
            {
                if (_localModels.Items.Where(x => x.LocalFilePath == file).Any()) continue;

                LocalModel model = new LocalModel() 
                {
                    LocalFilePath = file,
                    Name = Path.GetFileNameWithoutExtension(file),
                    LocalImagePath = GetImagePath(folder, Path.GetFileNameWithoutExtension(file)),
                    Type = type
                };
                _localModels.Add(model);
            }
        }
        private string GetImagePath(string folder, string filename)
        {
            var path = Path.Combine(folder, filename) + ".preview.png";
            if (File.Exists(path)) return path;
            else return  Path.Combine(Environment.CurrentDirectory,"Assets\\card-no-preview.png");
        }
        private void RemoveNotExists(IEnumerable<string> files, Types type)
        {
            if(files.Any())
            {
                var notexisted = _localModels.Items.Where(x => x.Type==type && !files.Contains(x.LocalFilePath));
                foreach (var file in notexisted)
                {
                    _localModels.Remove(file);
                }
            }
        }
    }
}
