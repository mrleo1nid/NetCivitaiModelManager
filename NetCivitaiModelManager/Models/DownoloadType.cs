using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Models
{
    public enum DownoloadType
    {
        [Description("Модель")]
        Model,
        [Description("Изображение")]
        Image,
        [Description("Другое")]
        Custom
    }
}
