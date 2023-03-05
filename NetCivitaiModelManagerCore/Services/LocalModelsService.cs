using DynamicData;
using NetCivitaiModelManagerCore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NetCivitaiModelManagerCore.Services
{
    public class LocalModelsService
    {
        // Объявляем изменяемый список сделок.
        private readonly SourceList<LocalModel> _models;

        // Выставляем наружу поток изменений коллекции.
        // Если ожидается, что будет более одного подписчика,
        // рекомендуется использовать оператор Publish() из Rx.
        public IObservable<IChangeSet<LocalModel>> Connect() => _models.Connect();

        public LocalModelsService()
        {
            _models = new SourceList<LocalModel>();
        }
    }
}
