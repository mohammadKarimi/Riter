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
        double width = Math.Abs(endPoint.X - startPoint.X);
        double height = Math.Abs(endPoint.Y - startPoint.Y);
        double radiusX = width / 2;
        double centerX = (startPoint.X + endPoint.X) / 2;
        double topY = startPoint.Y;
        double bottomY = startPoint.Y + height;

        List<Point> topEllipsePoints = GetEllipsePoints(centerX, topY, radiusX, true);

        List<Point> leftSideStylusPoints = [new(centerX - radiusX, topY)];
        List<Point> bottomEllipsePoints = GetEllipsePoints(centerX, bottomY, radiusX, false);
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
        int segments = 100;
        List<Point> points = [];
        double angleStep = Math.PI * 2 / segments;
        for (int i = 0; i <= segments; i++)
        {
            double angle = angleStep * i;
            double x = centerX + (radiusX * Math.Cos(angle));
            double y = centerY + (radiusX / 2 * Math.Sin(angle));
            if (!isTop && angle > Math.PI)
            {
                continue;
            }

            points.Add(new Point(x, y));
        }

        return points;
    }
}
