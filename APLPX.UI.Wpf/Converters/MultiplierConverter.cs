using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace APLPX.UI.WPF.Converters
{
    /// <summary>
    /// Adjusts an input value by a multiple. 
    /// value: the value to multiply.
    /// parameter: the multiple to apply to the value.
    /// </summary>
    public class MultiplierConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal multiple = System.Convert.ToDecimal(parameter);
            decimal inputValue = System.Convert.ToDecimal(value);

            decimal result = multiple * inputValue;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("This is a one-way conversion.");
        }
    }
}
