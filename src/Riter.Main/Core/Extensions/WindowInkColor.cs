using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Riter.Main.Core.Extensions;

/// <summary>
/// In this function we read the default color from setting object and set it to MainInkCanvas.
/// </summary>
public static class WindowInkColor
{
    /// <summary>
    /// In this function we read the default color from setting object and set it to MainInkCanvas.
    /// If there is no config in setting object, we set DefaultHexColor.
    /// </summary>
    /// <param name="mainWindow">The MainWindow instance.</param>
    /// <returns>Returns the modified MainWindow instance with the default colored attached.</returns>
    public static MainWindow SetDefaultColor(this MainWindow mainWindow)
    {
        var settings = App.ServiceProvider.GetService<AppSettings>();
        var customColor = (Color)ColorConverter.ConvertFromString(settings.InkDefaultColor);
        mainWindow.MainInkCanvas.DefaultDrawingAttributes.Color = customColor;
        return mainWindow;
    }
}
