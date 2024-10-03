using System.Windows.Ink;

namespace Riter.Main.Core.Extensions;

/// <summary>
/// Attaches event listeners to the MainWindow's InkCanvas for stroke changes.
/// </summary>
public static class WindowEventListeners
{
    /// <summary>
    /// Attaches event listeners to the MainWindow's InkCanvas for stroke changes.
    /// Specifically, it listens for changes to the strokes on the InkCanvas.
    /// </summary>
    /// <param name="mainWindow">The MainWindow instance where the event listeners will be attached.</param>
    /// <returns>Returns the modified MainWindow instance with the event listeners attached.</returns>
    public static MainWindow SetEventListeners(this MainWindow mainWindow)
    {
        mainWindow.MainInkCanvas.Strokes.StrokesChanged += StrokesChanged;
        return mainWindow;
    }

    /// <summary>
    /// Handles the StrokesChanged event when the user draws on the InkCanvas.
    /// This method will be used to track and store stroke changes in a stack for history purposes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains the stroke collection that has changed.</param>
    private static void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {
        
    }
}
