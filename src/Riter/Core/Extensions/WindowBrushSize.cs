using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;

namespace Riter.Core.Extensions;

/// <summary>
/// Sets the brush size for the InkCanvas based on the current editing mode.
/// </summary>
public static class WindowBrushSize
{
    /// <summary>
    /// Sets the brush size for the InkCanvas based on the current editing mode.
    /// If the editing mode is set to 'EraseByPoint', it adjusts the eraser size using an elliptical shape.
    /// Otherwise, it sets the default drawing brush size for drawing on the InkCanvas.
    /// </summary>
    /// <param name="mainWindow">The MainWindow instance where the brush size will be applied.</param>
    /// <param name="brushSize">size of brush.</param>
    /// <returns>Returns the modified MainWindow instance with the updated brush size.</returns>
    public static MainWindow SetBrushSize(this MainWindow mainWindow, BrushSize brushSize = BrushSize.S2X)
    {
        var brushSize_double = (double)brushSize;
        if (mainWindow.MainInkCanvas.EditingMode is InkCanvasEditingMode.EraseByPoint)
        {
            mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.GestureOnly;
            mainWindow.MainInkCanvas.EraserShape = new EllipseStylusShape(brushSize_double, brushSize_double);
            mainWindow.MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
        else
        {
            mainWindow.MainInkCanvas.DefaultDrawingAttributes.Height = brushSize_double;
            mainWindow.MainInkCanvas.DefaultDrawingAttributes.Width = brushSize_double;
        }

        return mainWindow;
    }
}
