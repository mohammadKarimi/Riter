using System.Windows.Ink;
using System.Windows.Media;

namespace Riter.Core;
public class RainbowStroke(StylusPointCollection stylusPoints) : Stroke(stylusPoints)
{
    protected override void DrawCore(DrawingContext drawingContext, DrawingAttributes drawingAttributes)
    {
        LinearGradientBrush rainbowBrush = new()
        {
            GradientStops =
            [
                new GradientStop(Colors.Red, 0),
                new GradientStop(Colors.Orange, 0.24),
                new GradientStop(Colors.Yellow, 0.39),
                new GradientStop(Colors.Green, 0.67),
                new GradientStop(Colors.Blue, 0.85),
                new GradientStop(Colors.Aqua, 1),
            ],
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0),
        };
        Geometry pathGeometry = GetGeometry(drawingAttributes);
        drawingContext.DrawGeometry(rainbowBrush, null, pathGeometry);
    }
}
