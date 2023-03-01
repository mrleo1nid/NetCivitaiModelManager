
using NetCivitaiModelManager.Extensions;
using System;
using System.Globalization;
using System.Windows.Data;

namespace NetCivitaiModelManager.Converters
{
    public class DownoloadStateToStringConverter : IValueConverter
    {
        public DownoloadStateToStringConverter()
        {
            
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum state)
            {
                return state.GetEnumDescription();
            }
            return null;
        }
       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
