using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPFCore.Converters
{
    /// <summary>Converts a bool to an image.</summary>
    [ValueConversion(typeof(bool), typeof(Image))]
    public class BoolToImageConverter : IValueConverter
    {
        /// <summary>Gets or sets the Image to be used to <c>true</c> values.</summary>
        public Image TrueImage { get; set; }

        /// <summary>Gets or sets the Image to be used to <c>false</c> values.</summary>
        public Image FalseImage { get; set; }

        /// <summary>Converts a bool to an <c>Image</c>.</summary>
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
            return (bool)value ? this.TrueImage : this.FalseImage;
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