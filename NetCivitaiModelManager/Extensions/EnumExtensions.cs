using CivitaiApiWrapper.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extensions
{
    public static class EnumExtensions
    {
        public static string GetFolderByType(this Types typesEnum, string basepath)
        {
            var result = string.Empty;
            switch (typesEnum)
            {
                case Types.LORA:
                    result = "models\\Lora";
                    break;
                case Types.TextualInversion:
                    result = "embeddings";
                    break;
                case Types.Hypernetwork:
                    result = "models\\hypernetworks";
                    break;
                case Types.Checkpoint:
                    result = "models\\Stable-diffusion";
                    break;
                case Types.AestheticGradient:
                    result = "extensions\\stable-diffusion-webui-aesthetic-gradients\\aesthetic_embeddings";
                    break;
                case Types.Controlnet:
                    result = "extensions\\sd-webui-controlnet\\models";
                    break;
                case Types.Poses:
                    result = "poses";
                    break;
            }
            return Path.Combine(basepath, result);
        }
    }
}
