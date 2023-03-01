using Akavache;
using Akavache.Sqlite3;
using Downloader;
using Microsoft.Extensions.Logging;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NetCivitaiModelManager.Services
{
    public class BlobCasheService
    {
        private ILogger<BlobCasheService> _logger;
        private SQLiteEncryptedBlobCache _blob;
        public  BlobCasheService(ILogger<BlobCasheService> logger, SQLiteEncryptedBlobCache sQLiteEncryptedBlobCache)
        {
            _logger = logger;
            _blob = sQLiteEncryptedBlobCache;
        }
        public async Task<string> GetHash(string key)
        {
           return await _blob.GetObject<string>(key)
                .Catch(Observable.Return(string.Empty));
        }
        public async Task InsertHash(string key, string hash)
        {
            await _blob.InsertObject(key, hash);
        }

        public void InsertDownoloadTask(string key, List<DownoloadTask> tasks)
        {
            try
            {
                _blob.InsertObject(key, tasks).Wait();
                foreach (DownoloadTask task in tasks)
                    if(task.DownloadService.Package != null)
                     _blob.InsertObject(task.Id.ToString(), task.DownloadService.Package).Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public async Task<List<DownoloadTask>> GetDownoloadTask(string key)
        {
            return await _blob.GetObject<List<DownoloadTask>>(key)
               .Catch(Observable.Return(new List<DownoloadTask>()));
        }
        public async Task<DownloadPackage?> GetDownoloadPack(string key)
        {
            try
            {
                return await _blob.GetObject<DownloadPackage>(key);
            }
            catch { return null; }
        }
    }
}

