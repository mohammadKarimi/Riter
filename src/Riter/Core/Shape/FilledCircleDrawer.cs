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
        double centerX = (startPoint.X + endPoint.X) / 2;
        double centerY = (startPoint.Y + endPoint.Y) / 2;
        double radius = Math.Sqrt(Math.Pow(endPoint.X - centerX, 2) + Math.Pow(endPoint.Y - centerY, 2));

        StrokeCollection strokes = new();

        for (double r = radius; r >= 0; r -= 1)
        {
            StylusPointCollection points = new();

            for (int angle = 0; angle <= 360; angle += 5)
            {
                double radians = angle * Math.PI / 180;
                double x = centerX + (r * Math.Cos(radians));
                double y = centerY + (r * Math.Sin(radians));
                points.Add(new StylusPoint(x, y));
            }

            Stroke stroke = new(points)
            {
                DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
            };

            strokes.Add(stroke);
        }

        Stroke filledStroke = MergeStrokes(strokes);
        return filledStroke;
    }

    private static Stroke MergeStrokes(StrokeCollection strokes)
    {
        StylusPointCollection allPoints = new();

        foreach (Stroke stroke in strokes)
        {
            allPoints.Add(stroke.StylusPoints);
        }

        return new Stroke(allPoints)
        {
            DrawingAttributes = strokes[0].DrawingAttributes.Clone(),
        };
    }
}
