using Akavache.Sqlite3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class BlobCashService
    {
        private SQLiteEncryptedBlobCache _sqlblob;
        public BlobCashService(SQLiteEncryptedBlobCache sQLiteEncryptedBlobCache) 
        {
           _sqlblob = sQLiteEncryptedBlobCache;
        }
    }
}
