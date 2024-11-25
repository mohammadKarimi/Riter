using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Riter.Core.Enum;

namespace Riter.Core.Drawing;
public class ColorToGradientConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string inkColor && inkColor == InkColor.RainBow.ToString())
        {
            return new LinearGradientBrush
            {
                GradientStops =
                [
                    new GradientStop(Colors.Red, 0),
                    new GradientStop(Colors.Orange, 0.16),
                    new GradientStop(Colors.Yellow, 0.33),
                    new GradientStop(Colors.Green, 0.5),
                    new GradientStop(Colors.Blue, 0.66),
                    new GradientStop(Colors.Indigo, 0.83),
                    new GradientStop(Colors.Violet, 1.0),
                ],
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
            };
        }
        else if (value is string colorName && !string.IsNullOrEmpty(colorName))
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorName));
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
