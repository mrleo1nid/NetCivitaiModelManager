using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public enum BaseSelectEnum
    {
        [Description("Все")]
        All,
        [Description("Да")]
        Yes,
        [Description("Нет")]
        No
    }
}
