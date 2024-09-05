using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Main.Core.Enum;

namespace Riter.Main.Core.Extensions;
public static class WindowBrushSize
{
    private const double DefaultSize = (double)BrushSize.S2X;

    /// <summary>
    /// Sets the brush size for the InkCanvas based on the current editing mode.
    /// If the editing mode is set to 'EraseByPoint', it adjusts the eraser size using an elliptical shape.
    /// Otherwise, it sets the default drawing brush size for drawing on the InkCanvas.
    /// </summary>
    /// <param name="mainWindow">The MainWindow instance where the brush size will be applied.</param>
    /// <returns>Returns the modified MainWindow instance with the updated brush size.</returns>
    public static MainWindow SetBrushSize(this MainWindow mainWindow)
    {
        if (mainWindow.MainInkCanvas.EditingMode is InkCanvasEditingMode.EraseByPoint)
        {
            mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.GestureOnly;
            mainWindow.MainInkCanvas.EraserShape = new EllipseStylusShape(DefaultSize, DefaultSize);
            mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
        else
        {
            mainWindow.MainInkCanvas.DefaultDrawingAttributes.Height = DefaultSize;
            mainWindow.MainInkCanvas.DefaultDrawingAttributes.Width = DefaultSize;
        }
        return mainWindow;
    }
}
