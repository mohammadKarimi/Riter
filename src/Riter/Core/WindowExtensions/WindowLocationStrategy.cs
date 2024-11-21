using System.Diagnostics;
using System.Windows.Controls;
using Riter.Core.Interfaces;
using Riter.Core.UI;

namespace Riter.Core.WindowExtensions;

public class BottomCenterStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        var canvasWidth = layout.ActualWidth;
        var canvasHeight = layout.ActualHeight;
        var paletteWidth = mainPalette.ActualWidth;
        var paletteHeight = mainPalette.ActualHeight;

        Canvas.SetLeft(mainPalette, (canvasWidth - paletteWidth) / 2);
        Canvas.SetTop(mainPalette, canvasHeight - paletteHeight - 75);
    }
}

public class BottomLeftStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        var canvasHeight = layout.ActualHeight;
        var paletteHeight = mainPalette.ActualHeight;

        Canvas.SetLeft(mainPalette, 75);
        Canvas.SetTop(mainPalette, canvasHeight - paletteHeight - 75);
    }
}

public class BottomRightStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        var canvasWidth = layout.ActualWidth;
        var canvasHeight = layout.ActualHeight;
        var paletteWidth = mainPalette.ActualWidth;
        var paletteHeight = mainPalette.ActualHeight;

        Canvas.SetLeft(mainPalette, canvasWidth - paletteWidth - 75);
        Canvas.SetTop(mainPalette, canvasHeight - paletteHeight - 75);
    }
}

public class CenterStrategy : IStartupLocationStrategy
{
    public void AdjustSize(Grid layout, StackPanel mainPalette, AppSettings appSettings)
    {
        var canvasWidth = layout.ActualWidth;
        var canvasHeight = layout.ActualHeight;
        var paletteWidth = mainPalette.ActualWidth;
        var paletteHeight = mainPalette.ActualHeight;
        Canvas.SetLeft(mainPalette, (canvasWidth - paletteWidth) / 2);
        Canvas.SetTop(mainPalette, (canvasHeight - paletteHeight - 440) / 2);
    }
}
