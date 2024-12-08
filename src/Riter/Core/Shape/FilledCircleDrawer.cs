using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;

public class FilledCircleDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.FilledCircle;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow)
    {
        var centerX = (startPoint.X + endPoint.X) / 2;
        var centerY = (startPoint.Y + endPoint.Y) / 2;
        var radius = Math.Sqrt(Math.Pow(endPoint.X - centerX, 2) + Math.Pow(endPoint.Y - centerY, 2));

        var strokes = new StrokeCollection();

        for (var r = radius; r >= 0; r -= 1)
        {
            var points = new StylusPointCollection();

            for (var angle = 0; angle <= 360; angle += 5)
            {
                var radians = angle * Math.PI / 180;
                var x = centerX + (r * Math.Cos(radians));
                var y = centerY + (r * Math.Sin(radians));
                points.Add(new StylusPoint(x, y));
            }

            var stroke = new Stroke(points)
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            };

            strokes.Add(stroke);
        }

        var filledStroke = MergeStrokes(strokes);
        return filledStroke;
    }

    private static Stroke MergeStrokes(StrokeCollection strokes)
    {
        var allPoints = new StylusPointCollection();

        foreach (var stroke in strokes)
        {
            allPoints.Add(stroke.StylusPoints);
        }

        return new Stroke(allPoints)
        {
            DrawingAttributes = strokes[0].DrawingAttributes.Clone(),
        };
    }
}
