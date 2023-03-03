using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CivitaiApiWrapper.Extension
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
                    else if (attribute.Description.Replace(" ","") == description)
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
        public static List<string> ToStringList<T>(this List<T> value) where T : Enum
        {
            var result = new List<string>();
            foreach (var item in value)
            {
                result.Add(item.GetEnumDescription());
            }
            return result;
        }

        public static List<T> ToEnumList<T>(this List<string> value) where T : Enum
        {
            var result = new List<T>();
            foreach (var item in value)
            {
                result.Add(item.ToEnum<T>());
            }
            return result;
        }
    }
}
