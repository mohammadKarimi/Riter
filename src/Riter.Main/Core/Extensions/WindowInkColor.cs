using System.Windows.Media;

namespace Riter.Main.Core.Extensions;

/// <summary>
/// In this function we read the default color from setting object and set it to MainInkCanvas.
/// </summary>
public static class WindowInkColor
{
    private const string DefaultHexColor = "#FFFF5656";

    /// <summary>
    /// In this function we read the default color from setting object and set it to MainInkCanvas.
    /// If there is no config in setting object, we set DefaultHexColor.
    /// </summary>
    /// <param name="mainWindow">The MainWindow instance</param>
    /// <returns>Returns the modified MainWindow instance with the default colored attached.</returns>
    public static MainWindow SetDefaultColor(this MainWindow mainWindow)
    {
        var customColor = (Color)ColorConverter.ConvertFromString(DefaultHexColor);
        mainWindow.MainInkCanvas.DefaultDrawingAttributes.Color = customColor;
        return mainWindow;
    }
}
