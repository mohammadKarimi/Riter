using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;
public class RectangleDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.Rectangle;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint)
    {
        var topLeft = new Point(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
        var topRight = new Point(Math.Max(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
        var bottomLeft = new Point(Math.Min(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y));
        var bottomRight = new Point(Math.Max(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y));
        var points = new List<Point> { topLeft, topRight, bottomRight, bottomLeft, topLeft };

        var stylusPoints = new StylusPointCollection(points);
        var newAttributes = canvas.DefaultDrawingAttributes.Clone();
        newAttributes.StylusTip = StylusTip.Rectangle;
        newAttributes.IgnorePressure = true;
        var stroke = new Stroke(stylusPoints)
        {
            DrawingAttributes = newAttributes,
        };

        return stroke;
    }
}
