using System;
using System.Globalization;
using System.Windows.Data;

namespace FSTEC.Converters
{
    public class FromNullToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((int)value == 0)
            {
                return "Угроза не выбрана";
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
