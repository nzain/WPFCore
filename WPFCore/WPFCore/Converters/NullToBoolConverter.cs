using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>An object to bool converter.</summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class NullToBoolConverter : IValueConverter
    {
        /// <summary>Gets or sets the bool value to use for <c>null</c> objects.</summary>
        public bool NullValue { get; set; }

        /// <summary>Converts an object to a bool.</summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? this.NullValue : !this.NullValue;
        }

        /// <summary>Not supported.</summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}