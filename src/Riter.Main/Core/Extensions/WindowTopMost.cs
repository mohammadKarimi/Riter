namespace Riter.Main.Core.Extensions;

/// <summary>
/// Sets the window's Topmost property to control whether the window is always on top of other windows.
/// </summary>
public static class WindowTopMost
{
    /// <summary>
    /// Sets the window's Topmost property to control whether the window is always on top of other windows.
    /// </summary>
    /// <param name="mainWindow">The MainWindow instance to modify.</param>
    /// <param name="enable">A boolean value indicating whether to enable or disable the Topmost property.</param>
    /// <returns>Returns the modified MainWindow instance.</returns>
    public static MainWindow SetTopMost(this MainWindow mainWindow, bool enable = true)
    {
        mainWindow.Topmost = enable;
        return mainWindow;
    }
}
