using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>Bool to opposite bool converter.</summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BoolInverter : IValueConverter
    {
        /// <summary>Converts a bool to the opposite bool.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            throw new InvalidOperationException("The target must be a boolean");
        }

        /// <summary>Converts the bool (back) to the opposite bool.</summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            throw new InvalidOperationException("The target must be a boolean");
        }
    }
}