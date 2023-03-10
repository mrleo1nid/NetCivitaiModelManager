using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extensions
{
    public class ApcExceptionHandler : IObserver<Exception>
    {
        private readonly IFullLogger _logger;

        public ApcExceptionHandler(IFullLogger logger)
        {
            _logger = logger;
        }

        public void OnCompleted()
        {
            if (Debugger.IsAttached) Debugger.Break();
        }

        public void OnError(Exception error)
        {
            if (Debugger.IsAttached) Debugger.Break();
            _logger.Error($"{error.Source}: {error.Message}", error);
        }

        public void OnNext(Exception value)
        {
            if (Debugger.IsAttached) Debugger.Break();

            _logger?.Error($"{value.Source}: {value.Message}", value);
        }
    }
}
