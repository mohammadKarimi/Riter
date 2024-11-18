using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;
public class CircleDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.Circle;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint)
    {
        var centerX = (startPoint.X + endPoint.X) / 2;
        var centerY = (startPoint.Y + endPoint.Y) / 2;
        var radius = Math.Sqrt(Math.Pow(endPoint.X - centerX, 2) + Math.Pow(endPoint.Y - centerY, 2));

        var points = new StylusPointCollection();
        for (var i = 0; i <= 360; i += 5)
        {
            var radians = i * Math.PI / 180;
            var x = centerX + (radius * Math.Cos(radians));
            var y = centerY + (radius * Math.Sin(radians));
            points.Add(new StylusPoint(x, y));
        }

        var stroke = new Stroke(points)
        {
            DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
        };
        return stroke;
    }
}
