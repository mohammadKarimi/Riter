using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;
public class CircleDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.Circle;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow = false)
    {
        double centerX = (startPoint.X + endPoint.X) / 2;
        double centerY = (startPoint.Y + endPoint.Y) / 2;
        double radius = Math.Sqrt(Math.Pow(endPoint.X - centerX, 2) + Math.Pow(endPoint.Y - centerY, 2));

        StylusPointCollection points = [];
        for (int i = 0; i <= 360; i += 5)
        {
            double radians = i * Math.PI / 180;
            double x = centerX + (radius * Math.Cos(radians));
            double y = centerY + (radius * Math.Sin(radians));
            points.Add(new StylusPoint(x, y));
        }

        return !isRainbow
            ? new Stroke(points)
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            }
            : new RainbowStroke(points)
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            };
    }
}
