using Akavache;
using Akavache.Sqlite3;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
        public BitmapImage GetImage(string key)
        {
            BitmapImage  bitmapImage = null;
            try
            {
                bitmapImage = _blob.GetObject<BitmapImage>(key).Wait();
            }
            catch
            {
                bitmapImage = new BitmapImage(new Uri(key));
                _blob.InsertObject(key, bitmapImage);
            }
            return bitmapImage;
        }
    }
}

