using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace WPFCore.Converters
{
    /// <summary>Converts a bool to an imagesource.</summary>
    [ValueConversion(typeof(bool), typeof(ImageSource))]
    public class BoolToImageSourceConverter : IValueConverter
    {
        /// <summary>Gets or sets the ImageSource to be used to <c>true</c> values.</summary>
        public ImageSource TrueImageSource { get; set; }

        /// <summary>Gets or sets the ImageSource to be used to <c>false</c> values.</summary>
        public ImageSource FalseImageSource { get; set; }

        /// <summary>Converts a bool to an <c>ImageSource</c>.</summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The optional parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return null;
            }
            return (bool)value ? this.TrueImageSource : this.FalseImageSource;
        }

        /// <summary>Not supported.</summary>
        /// <param name="value">The value.</param>
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