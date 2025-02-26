using System.Windows.Controls;
using Riter.Core.Interfaces;

namespace Riter.Core.WindowExtensions;

public class BottomCenterStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        double canvasWidth = layout.ActualWidth;
        double canvasHeight = layout.ActualHeight;
        double paletteWidth = mainPalette.ActualWidth;
        double paletteHeight = mainPalette.ActualHeight;

        Canvas.SetLeft(mainPalette, (canvasWidth - paletteWidth) / 2);
        Canvas.SetTop(mainPalette, canvasHeight - paletteHeight - 75);
    }
}

public class BottomLeftStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        double canvasHeight = layout.ActualHeight;
        double paletteHeight = mainPalette.ActualHeight;

        Canvas.SetLeft(mainPalette, 75);
        Canvas.SetTop(mainPalette, canvasHeight - paletteHeight - 75);
    }
}

public class BottomRightStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        double canvasWidth = layout.ActualWidth;
        double canvasHeight = layout.ActualHeight;
        double paletteWidth = mainPalette.ActualWidth;
        double paletteHeight = mainPalette.ActualHeight;

        Canvas.SetLeft(mainPalette, canvasWidth - paletteWidth - 75);
        Canvas.SetTop(mainPalette, canvasHeight - paletteHeight - 75);
    }
}

public class CenterStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        double canvasWidth = layout.ActualWidth;
        double canvasHeight = layout.ActualHeight;
        double paletteWidth = mainPalette.ActualWidth;
        double paletteHeight = mainPalette.ActualHeight;
        Canvas.SetLeft(mainPalette, (canvasWidth - paletteWidth) / 2);
        Canvas.SetTop(mainPalette, (canvasHeight - paletteHeight - 440) / 2);
    }
}
