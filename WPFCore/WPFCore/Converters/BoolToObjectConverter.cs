using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFCore.Converters
{
    [ValueConversion(typeof(bool), typeof(object))]
    public class BoolToObjectConverter : IValueConverter
    {
        /// <summary>Gets or sets the TrueValue.</summary>
        public object TrueValue { get; set; }

        /// <summary>Gets or sets the FalseValue.</summary>
        public object FalseValue { get; set; }

        /// <summary>Gets or sets the NullValue.</summary>
        public object NullValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.NullValue;
            }
            return (bool)value ? this.TrueValue : this.FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
