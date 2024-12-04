using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Idibri.RevitPlugin.Common.Converters
{
    public class StringEqualityToVisibilityConverter : IValueConverter
    {
        public bool Reversed
        {
            get { return _reversed; }
            set { _reversed = value; }
        }
        private bool _reversed = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value != null && parameter != null)
            {
                if (value.ToString() == parameter.ToString())
                {
                    if (!Reversed)
                    {
                        return Visibility.Visible;
                    }
                    return Visibility.Collapsed;
                }
                else
                {
                    if (Reversed)
                    {
                        return Visibility.Visible;
                    }
                    return Visibility.Collapsed;
                }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
