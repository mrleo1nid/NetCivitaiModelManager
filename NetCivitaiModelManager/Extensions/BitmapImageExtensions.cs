using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NetCivitaiModelManager.Extensions
{
    public static class BitmapImageExtensions
    {
        public static BitmapImage ToBitmapImage(this byte[] bytes)
        {
            var bi = new BitmapImage();

            using (var fs = new MemoryStream(bytes))
            {
                bi.BeginInit();
                bi.StreamSource = fs;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();
            }

            bi.Freeze(); //Important to freeze it, otherwise it will still have minor leaks

            return bi;
        }
    }
}
