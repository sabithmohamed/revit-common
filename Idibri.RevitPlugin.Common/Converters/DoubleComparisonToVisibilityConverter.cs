using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Idibri.RevitPlugin.Common.Converters
{
    public class DoubleComparisonToVisibilityConverter : IValueConverter
    {
        public ComparisonType ComparisonType { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            if (value == null || parameter == null)
            {
                return Visibility.Collapsed;
            }

            double compare;
            double compareTo;
            bool passes = false;

            if (!double.TryParse(value.ToString(), out compare))
            {
                return Visibility.Collapsed;
            }
            if (!double.TryParse(parameter.ToString(), out compareTo))
            {
                return Visibility.Collapsed;
            }

            switch (ComparisonType)
            {
                case ComparisonType.Equal:
                    passes = compare == compareTo;
                    break;
                case ComparisonType.NotEqual:
                    passes = compare != compareTo;
                    break;
                case ComparisonType.LessThan:
                    passes = compare < compareTo;
                    break;
                case ComparisonType.LessThanEqual:
                    passes = compare <= compareTo;
                    break;
                case ComparisonType.GreaterThan:
                    passes = compare > compareTo;
                    break;
                case ComparisonType.GreaterThanEqual:
                    passes = compare >= compareTo;
                    break;
            }

            return passes ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
