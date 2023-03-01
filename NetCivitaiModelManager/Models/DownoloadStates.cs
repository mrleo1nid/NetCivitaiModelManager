using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public enum DownoloadStates
    {
        Created,
        Completed,
        Downoloading,
        Cancel,
        Paused
    }
}
