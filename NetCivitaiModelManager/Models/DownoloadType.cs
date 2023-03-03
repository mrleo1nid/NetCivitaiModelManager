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
        [Description("Архив поз")]
        PoseArch,
        [Description("Другое")]
        Custom,
        [Description("Модель LORA")]
        Lora,
        [Description("Текстовая инверсия")]
        TextualInversion,
        [Description("VAE")]
        Vae,
        [Description("Эстетический градиент")]
        AestheticGradient,
        [Description("Гиперсеть")]
        Hypenetwork
    }
}
