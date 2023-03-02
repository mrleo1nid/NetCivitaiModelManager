﻿using Akavache;
using CommunityToolkit.Mvvm.DependencyInjection;
using NetCivitaiModelManager.Services;
using NetCivitaiModelManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Cache;
using System.Reactive.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace NetCivitaiModelManager.Converters
{
    public class UrlToBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && !string.IsNullOrEmpty(str))
            {
                try
                {
                    return new BitmapImage(new Uri(str), new System.Net.Cache.RequestCachePolicy(RequestCacheLevel.CacheIfAvailable));
                }catch { return null; }
               
            }
            return null;
        }
       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
