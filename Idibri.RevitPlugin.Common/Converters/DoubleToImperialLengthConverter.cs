using System;
using System.Globalization;
using System.Windows.Data;

namespace Idibri.RevitPlugin.Common.Converters
{
    public class DoubleToImperialLengthConverter : IValueConverter
    {
        private static int[] Denominators = new int[] { 1, 2, 3, 4, 6, 8 }; //, 16, 32, 64 };
        private static readonly string InchesFormatString = "#.##";

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value == null) { return null; }
            double inches;
            if (double.TryParse(value.ToString(), out inches))
            {
                return (new ImperialLength(0, inches)).ToString(Denominators, 0.01, InchesFormatString);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value == null) { return null; }
            return ImperialLength.TryParse(value.ToString());
        }
    }
}
