using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;

public class FilledRectangleDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.FilledRectangle;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow = false)
    {
        Point topLeft = new(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y));
        Point bottomRight = new(Math.Max(startPoint.X, endPoint.X), Math.Max(startPoint.Y, endPoint.Y));

        StylusPointCollection stylusPoints = [];

        for (double x = topLeft.X; x <= bottomRight.X; x++)
        {
            for (double y = topLeft.Y; y <= bottomRight.Y; y++)
            {
                stylusPoints.Add(new StylusPoint(x, y));
            }
        }

        DrawingAttributes newAttributes = canvas.DefaultDrawingAttributes.Clone();
        newAttributes.StylusTip = StylusTip.Rectangle;
        newAttributes.IgnorePressure = true;

        return new Stroke(stylusPoints)
        {
            DrawingAttributes = newAttributes,
        };
    }
}
