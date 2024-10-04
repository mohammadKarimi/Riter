using System.Windows.Controls;
using System.Windows.Media;

namespace Riter.Core.Extensions;

/// <summary>
/// This Extension is aimed for adding dragable feature into MainWindow.
/// </summary>
public static class WindowDragableExtensions
{
    private static Point _lastMousePosition;
    private static bool _isDragging;

    /// <summary>
    /// Attaches drag functionality to a given UIElement (like Canvas or Panel).
    /// </summary>
    /// <param name="mainWindow">The window containing the element, used for mouse position calculations.</param>
    /// <param name="element">The element that you want to make draggable (e.g., MainPallete).</param>
    /// <returns>MainWindow to follow Fluent chain.</returns>
    public static MainWindow EnableDragging(this MainWindow mainWindow, UIElement element)
    {
        mainWindow.MouseDown += (sender, e) => StartDrag(mainWindow, element);
        mainWindow.MouseMove += (sender, e) => PerformDrag(mainWindow, element, e);
        mainWindow.MouseUp += (sender, e) => EndDrag(element);
        mainWindow.MouseLeave += (sender, e) => EndDrag(element);
        return mainWindow;
    }

    /// <summary>
    /// Begins the drag operation by capturing the mouse position.
    /// </summary>
    private static void StartDrag(Window window, UIElement element)
    {
        _lastMousePosition = Mouse.GetPosition(window);
        _isDragging = true;
        element.SetValue(Panel.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
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

        var currentMousePosition = Mouse.GetPosition(window);
        var offset = currentMousePosition - _lastMousePosition;

        if (element is FrameworkElement frameworkElement)
        {
            var newTop = Canvas.GetTop(frameworkElement) + offset.Y;
            var newLeft = Canvas.GetLeft(frameworkElement) + offset.X;

            Canvas.SetTop(frameworkElement, newTop);
            Canvas.SetLeft(frameworkElement, newLeft);
        }

        _lastMousePosition = currentMousePosition;
    }

    /// <summary>
    /// Ends the drag operation by resetting the background and stopping the drag.
    /// </summary>
    private static void EndDrag(UIElement element)
    {
        _isDragging = false;
        element.ClearValue(Panel.BackgroundProperty);
    }
}
