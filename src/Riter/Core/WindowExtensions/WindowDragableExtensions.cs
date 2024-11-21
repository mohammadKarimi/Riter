using System.Windows.Media;

namespace Riter.Core.WindowExtensions;

/// <summary>
/// This Extension is aimed for adding dragable feature into MainWindow.
/// </summary>
public static class WindowDraggableExtensions
{
    private static Point _lastMousePosition;
    private static bool _isDragging;

    /// <summary>
    /// Attaches drag functionality to a given UIElement (like Canvas or Panel).
    /// </summary>
    /// <param name="mainWindow">The window containing the element, used for mouse position calculations.</param>
    /// <param name="element">The element that you want to make draggable (e.g., MainPalette).</param>
    /// <returns>MainWindow to follow Fluent chain.</returns>
    public static MainWindow EnableDragging(this MainWindow mainWindow, UIElement element)
    {
        mainWindow.WindowControl.MoveButton.MouseDown += (sender, e) => StartDrag(mainWindow, element, e);
        mainWindow.MouseMove += (sender, e) => PerformDrag(mainWindow, element, e);
        mainWindow.MouseUp += (sender, e) => EndDrag(mainWindow);
        return mainWindow;
    }

    /// <summary>
    /// Begins the drag operation by capturing the mouse position.
    /// </summary>
    private static void StartDrag(Window window, UIElement element, MouseButtonEventArgs e)
    {
        _lastMousePosition = e.GetPosition(window);
        _isDragging = true;

        Mouse.Capture(window);
        window.Cursor = Cursors.SizeAll;

        if (element.RenderTransform is not TranslateTransform)
        {
            element.RenderTransform = new TranslateTransform();
        }
    }

    /// <summary>
    /// Performs the dragging operation by updating the position of the element.
    /// </summary>
    private static void PerformDrag(Window window, UIElement element, MouseEventArgs e)
    {
        if (!_isDragging)
        {
            return;
        }

        var currentMousePosition = e.GetPosition(window);
        var offset = currentMousePosition - _lastMousePosition;

        if (element.RenderTransform is TranslateTransform transform)
        {
            transform.X += offset.X;
            transform.Y += offset.Y;
        }

        _lastMousePosition = currentMousePosition;
    }

    /// <summary>
    /// Ends the drag operation by releasing the mouse capture and resetting the cursor.
    /// </summary>
    private static void EndDrag(Window window)
    {
        _isDragging = false;
        Mouse.Capture(null);
        window.Cursor = Cursors.Arrow;
    }
}
