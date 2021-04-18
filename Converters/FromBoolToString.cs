using System;
using System.Globalization;
using System.Windows.Data;

namespace FSTEC
{
    class FromBoolToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return "Да";
            }
            else
            {
                return "Нет";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Да")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
