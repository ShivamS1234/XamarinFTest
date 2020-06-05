using System;
using System.Globalization;
using Xamarin.Forms;

namespace XFTest.Converters
{
    public class BoolToValueConverter:IValueConverter
    {
        public BoolToValueConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return false;
            else
                return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
