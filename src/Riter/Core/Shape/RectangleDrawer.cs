using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;
public class RectangleDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.Rectangle;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow = false)
    {
        Point topLeft = new(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
        Point topRight = new(Math.Max(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
        Point bottomLeft = new(Math.Min(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y));
        Point bottomRight = new(Math.Max(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y));
        List<Point> points = [topLeft, topRight, bottomRight, bottomLeft, topLeft];

        StylusPointCollection stylusPoints = new(points);
        DrawingAttributes newAttributes = canvas.DefaultDrawingAttributes.Clone();
        newAttributes.StylusTip = StylusTip.Rectangle;
        newAttributes.IgnorePressure = true;
        return !isRainbow
            ? new Stroke(stylusPoints)
            {
                DrawingAttributes = newAttributes,
            }
            : new RainbowStroke(stylusPoints)
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            };
    }
}
