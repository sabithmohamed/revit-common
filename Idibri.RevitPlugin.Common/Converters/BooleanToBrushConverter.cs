using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Idibri.RevitPlugin.Common.Converters
{
    public class BooleanToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; }
        public Brush FalseBrush { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    return TrueBrush;
                }
                else
                {
                    return FalseBrush;
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
