using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public enum DownoloadStates
    {
        [Description("Создан")]
        Created,
        [Description("Завершен")]
        Completed,
        [Description("Загрузка")]
        Downoloading,
        [Description("Отменен")]
        Cancel,
        [Description("Пауза")]
        Paused
    }
}
