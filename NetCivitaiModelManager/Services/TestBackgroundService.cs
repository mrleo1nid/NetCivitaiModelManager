using DynamicData;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class TestBackgroundService 
    {
        // Объявляем изменяемый список сделок.
        private readonly SourceList<LocalModel> _trades;

        // Выставляем наружу поток изменений коллекции.
        // Если ожидается, что будет более одного подписчика,
        // рекомендуется использовать оператор Publish() из Rx.
        public IObservable<IChangeSet<LocalModel>> Connect() => _trades.Connect();

        public TestBackgroundService()
        {
            _trades = new SourceList<LocalModel>();
           Task.Factory.StartNew(() => {
               while(true)
               {
                   _trades.Add(new LocalModel());
                   Thread.Sleep(1000);
               }
               
           });
        }
      
    }
}
