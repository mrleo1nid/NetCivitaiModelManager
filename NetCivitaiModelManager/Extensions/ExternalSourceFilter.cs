using DynamicData.Binding;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetCivitaiModelManager.Models;
using CivitaiApiWrapper.Enums;
using System.Reactive.Linq;

namespace NetCivitaiModelManager.Extensions
{
    public class ExternalSourceFilter : AbstractNotifyPropertyChanged, IDisposable
    {
        private readonly IDisposable _cleanUp;

        public IObservableList<LocalModel> Filtered { get; }

        public ExternalSourceFilter(IObservableList<LocalModel> source, IObservableList<Types> families)
        {
            /*
             *  Create list which is filtered from the result of another filter
            */

            var familyFilter = families.Connect()
                .ToCollection()
                .Select(items =>
                {
                    bool Predicate(LocalModel animal) => items.Contains(animal.Type);
                    return (Func<LocalModel, bool>)Predicate;
                });

            Filtered = source.Connect()
                .Filter(familyFilter)
                .AsObservableList();

            _cleanUp = Filtered;
        }

        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }
}
