using System.Windows.Ink;
using System.Windows.Media;
using Riter.Core.Enum;

namespace Riter.Core.Drawing;

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
            Color = color == InkColor.RainBow.ToString() ? (Color)ColorConverter.ConvertFromString("#FFFF5656") : (Color)ColorConverter.ConvertFromString(color),
            Height = isHighlighter ? size * 5 : size,
            Width = isHighlighter ? size * 5 : size,
            IsHighlighter = isHighlighter,
            IgnorePressure = true,
            StylusTip = StylusTip.Ellipse,
        };

        return drawingAttributes;
    }
}
