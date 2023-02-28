using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Services
{
    public partial class BaseQuequeService : ObservableObject
    {
        [ObservableProperty]
        private string? currentName;
        [ObservableProperty]
        private long currentFullSize;
        [ObservableProperty]
        private long currentProgress;
        [ObservableProperty]
        private int ququeCount;
        [ObservableProperty]
        public bool isStarted;
    }
}
