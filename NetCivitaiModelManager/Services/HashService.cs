using Blake3Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using NetCivitaiModelManager.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public partial class HashService : BaseQuequeService
    {
        public delegate void HashCompleteHandler(LocalFile file);
        public event HashCompleteHandler? NotifyHashComplete;

        public List<LocalFile> Quque { get; private set; } = new List<LocalFile>();

        private LocalFile? currentFile;
        private ILogger<HashService> _logger;
        private BlobCasheService _blobCasheService;

        [ObservableProperty]
        private string allProgress = string.Empty;
        public HashService(ILogger<HashService> logger, BlobCasheService blobCasheService)
        {
            _logger = logger;
            _blobCasheService = blobCasheService;
            IsStarted = false;
        }
        public void AddToQuque(LocalFile localFile)
        {
            if(!Quque.Contains(localFile))
                Quque.Add(localFile);
        }
        public void Start()
        {
            if(!IsStarted)
            {
                IsStarted = true;
                QuqueCount = Quque.Count;
                Task.Factory.StartNew(RefreshModel);
            }
        }
        private async Task RefreshModel()
        {
            if (currentFile == null)
            {
                if (Quque.Any())
                {
                    currentFile = Quque.FirstOrDefault();
                    CurrentName = currentFile.Name;
                    await CalculateModelHash();
                }
                else
                {
                    if (IsStarted)
                        IsStarted = false;
                }
            }
        }
       
        public async Task CalculateModelHash()
        {
            if (!string.IsNullOrEmpty(currentFile?.Hash))
                return;

            var identifier = GetFileIdentifier(currentFile.FullName);
            var hash = await _blobCasheService.GetHash(identifier);
            if(string.IsNullOrEmpty(hash))
            {
                using (var stream = File.OpenRead(currentFile.FullName))
                    hash = await GetHashAsync(stream);
                await _blobCasheService.InsertHash(identifier, hash);
            }
            currentFile.Hash = hash;
            currentFile.HashRedy = true;
            NotifyHashComplete?.Invoke(currentFile);
            Quque.Remove(currentFile);
            currentFile = null;
            QuqueCount = Quque.Count;
            await RefreshModel();
        }
       
        private async Task<string> GetHashAsync(Stream stream)
        {
            CurrentFullSize = stream.Length;
            CurrentProgress = 0;
            StringBuilder sb;
            using (var algo = new Blake3())
            {
                var buffer = new byte[8192];
                int bytesRead;
                // compute the hash on 8KiB blocks
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    algo.TransformBlock(buffer, 0, bytesRead, buffer, 0);
                    CurrentProgress += bytesRead;
                }
                algo.TransformFinalBlock(buffer, 0, bytesRead);

                // build the hash string
                sb = new StringBuilder(algo.HashSize / 4);
                for(int i = 0; i < 32; i++)
                    sb.AppendFormat("{0:x2}", algo.Hash[i]);
            }
            return sb.ToString().ToUpper();
        }
        private string GetFileIdentifier(string filepath)
        {
            FileInfo file = new FileInfo(filepath);
            return $"{filepath}_{file.Length}_{file.LastWriteTime.ToString("g")}";
        }
    }
}
