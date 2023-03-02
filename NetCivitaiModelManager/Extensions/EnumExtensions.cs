using CivitaiApi.CivitaiRequestParams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }
            return value.ToString();
        }
        public static T ToEnum<T>(this string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }
        public static string GetFolderByType(this TypesEnum typesEnum, string basepath)
        {
            var result = string.Empty;
            switch (typesEnum)
            {
                case TypesEnum.LORA:
                    result = "models\\Lora";
                    break;
                case TypesEnum.TextualInversion:
                    result = "embeddings";
                    break;
                case TypesEnum.Hypernetwork:
                    result = "models\\hypernetworks";
                    break;
                case TypesEnum.Checkpoint:
                    result = "models\\Stable-diffusion";
                    break;
                case TypesEnum.AestheticGradient:
                    result = "extensions\\stable-diffusion-webui-aesthetic-gradients\\aesthetic_embeddings";
                    break;
                case TypesEnum.Controlnet:
                    result = "extensions\\sd-webui-controlnet\\models";
                    break;
                case TypesEnum.Poses:
                    result = "poses";
                    break;
            }
            return Path.Combine(basepath, result);
        }
    }
}
