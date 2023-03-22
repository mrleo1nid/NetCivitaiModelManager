using DynamicData;
using NetCivitaiModelManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public class DownoloadsService
    {
        private readonly ConfigService _configService;

        private readonly SourceList<DownoloadItem> _downoloads;
        public IObservable<IChangeSet<DownoloadItem>> Connect() => _downoloads.Connect();

        public DownoloadsService(ConfigService configService) 
        {
            _configService = configService;
            _downoloads = new SourceList<DownoloadItem>();
        }
    }
}
