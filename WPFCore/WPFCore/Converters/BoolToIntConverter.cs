using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>Converter from bool to int.</summary>
    [ValueConversion(typeof(bool), typeof(int))]
    public class BoolToIntConverter : IValueConverter
    {
        /// <summary>Converts a bool to int.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                var boolValue = (bool)value;
                return boolValue ? 1 : 0;
            }
            string msg = string.Format("expected bool but got {0}", value != null ? value.GetType().Name : "null");
            throw new ArgumentException(msg, "value");
        }

        /// <summary>Converts the int back to a bool.</summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var intValue = (int)value;
                return intValue > 0;
            }
            string msg = string.Format("expected int but got {0}", value != null ? value.GetType().Name : "null");
            throw new ArgumentException(msg, "value");
        }
    }
}