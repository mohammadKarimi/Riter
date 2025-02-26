using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;

public class LineDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.Line;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow = false)
    {
        StylusPointCollection stylusPoints = new(new[]
        {
            new StylusPoint(startPoint.X, startPoint.Y),
            new StylusPoint(endPoint.X, endPoint.Y),
        });

        return !isRainbow
            ? new Stroke(stylusPoints)
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            }
            : new RainbowStroke(stylusPoints)
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            };
    }
}
