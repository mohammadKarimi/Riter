using System.Windows.Ink;
using System.Windows.Media;

namespace Riter.Core;

/// <summary>
/// Factory for creating Drawing Attribute instance.
/// </summary>
public static class DrawingAttributesFactory
{
    /// <summary>
    /// Factory for creating Drawing Attribute instance.
    /// </summary>
    /// <param name="color">color of ink.</param>
    /// <param name="size">size of ink.</param>
    /// <param name="isHighlighter">is pen ot highlighter.</param>
    /// <returns>Canvas Ink Attributes.</returns>
    public static DrawingAttributes CreateDrawingAttributes(string color, double size, bool isHighlighter)
    {
        var drawingAttributes = new DrawingAttributes
        {
            Color = (Color)ColorConverter.ConvertFromString(color),
            Height = isHighlighter ? size * 2.5 : size,
            Width = isHighlighter ? size * 2.5 : size,
            IsHighlighter = isHighlighter,
            IgnorePressure = true,
            StylusTip = isHighlighter ? StylusTip.Rectangle : StylusTip.Ellipse,
        };

        return drawingAttributes;
    }
}
