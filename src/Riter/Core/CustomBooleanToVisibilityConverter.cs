using System.Globalization;
using System.Windows.Data;

namespace Riter.Core;
public class CustomBooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is bool boolValue ? boolValue ? Visibility.Collapsed : Visibility.Visible : (object)Visibility.Visible;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => value is Visibility visibility ? visibility == Visibility.Visible : (object)false;
}
