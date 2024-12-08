using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Riter.Core.Drawing;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Core.Shape;
public class ArrowDrawer : IShapeDrawer
{
    public DrawingShape SupportedShape => DrawingShape.Arrow;

    public Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow = false)
    {
        var lineAttributes = canvas.DefaultDrawingAttributes.Clone();
        lineAttributes.StylusTip = StylusTip.Ellipse;
        lineAttributes.IgnorePressure = true;

        var points = new List<Point> { startPoint, endPoint };

        points.AddRange(CreateArrowheadPoints(startPoint, endPoint, 15));
        points.AddRange(CreateArrowheadPoints(startPoint, endPoint, -15));

        if (!isRainbow)
            return new Stroke(new StylusPointCollection(points)) { DrawingAttributes = lineAttributes };

        var stroke = new RainbowStroke(new StylusPointCollection(points))
        {
            DrawingAttributes = canvas.DefaultDrawingAttributes.Clone(),
        };

        return stroke;
    }

    private static List<Point> CreateArrowheadPoints(Point startPoint, Point endPoint, double rotation)
    {
        List<Point> arrowPoints = [];

        VectorX ps = new(startPoint.X, startPoint.Y);
        VectorX pe = new(endPoint.X, endPoint.Y);
        var arrowPoint = (((ps - pe) * 0.85f) + ps).ToPoint();

        var rotatingMatrix = default(Matrix);
        rotatingMatrix.RotateAt(rotation, endPoint.X, endPoint.Y);

        var rotatedArrowPoint = rotatingMatrix.Transform(arrowPoint);
        arrowPoints.Add(endPoint);
        arrowPoints.Add(rotatedArrowPoint);

        return arrowPoints;
    }
}
