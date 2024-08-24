using System.Globalization;
using System.Windows.Data;

namespace Riter.Main.Core;
public class CenterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double canvasWidth && parameter is double gridWidth)
        {
            return (canvasWidth - gridWidth) / 2;
        }
        return 0; // Default to 0 if the types are not as expected
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
