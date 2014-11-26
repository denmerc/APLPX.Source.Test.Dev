using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace APLPX.UI.WPF.Converters
{
    /// <summary>
    /// Inverts a boolean input value. 
    /// true => false; 
    /// false => true.
    /// </summary>
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool inputValue = System.Convert.ToBoolean(value);

            return !inputValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool inputValue = System.Convert.ToBoolean(value);

            return !inputValue;
        }
    }
}
