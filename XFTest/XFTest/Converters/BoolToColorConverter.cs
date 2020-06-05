using System;
using System.Globalization;
using Xamarin.Forms;

namespace XFTest.Converters
{
    public class BoolToColorConverter:IValueConverter
    {
        public string TrueColor { get; set; }
        public string FalseColor { get; set; }

  
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return TrueColor;
            else
                return FalseColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
