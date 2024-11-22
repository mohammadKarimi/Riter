using System.IO;
using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;

public class DatabaseDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.Database;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow = false)
    {
        var width = Math.Abs(endPoint.X - startPoint.X);
        var height = Math.Abs(endPoint.Y - startPoint.Y);
        var radiusX = width / 2;
        var centerX = (startPoint.X + endPoint.X) / 2;
        var topY = startPoint.Y;
        var bottomY = startPoint.Y + height;

        var topEllipsePoints = GetEllipsePoints(centerX, topY, radiusX, true);

        List<Point> leftSideStylusPoints = [new(centerX - radiusX, topY)];
        var bottomEllipsePoints = GetEllipsePoints(centerX, bottomY, radiusX, false);
        List<Point> combinedPoints = [.. topEllipsePoints, .. bottomEllipsePoints, .. leftSideStylusPoints];
        return !isRainbow
            ? new Stroke(new StylusPointCollection(combinedPoints))
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            }
            : new RainbowStroke(new StylusPointCollection(combinedPoints))
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            };
    }

    private static List<Point> GetEllipsePoints(double centerX, double centerY, double radiusX, bool isTop)
    {
        var segments = 100;
        List<Point> points = new List<Point>();
        var angleStep = Math.PI * 2 / segments;
        for (var i = 0; i <= segments; i++)
        {
            var angle = angleStep * i;
            var x = centerX + (radiusX * Math.Cos(angle));
            var y = centerY + (radiusX / 2 * Math.Sin(angle));
            if (!isTop && angle > Math.PI)
            {
                continue;
            }

            points.Add(new Point(x, y));
        }

        return points;
    }
}
