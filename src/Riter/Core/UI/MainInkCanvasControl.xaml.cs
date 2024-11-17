using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter.Core.UI;

/// <summary>
/// Interaction logic for MainInkCanvasControl.xaml.
/// </summary>
public partial class MainInkCanvasControl : UserControl
{
    private readonly IStrokeHistoryService _strokeHistoryService;
    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler;
    private bool _isMoving = false;
    private Point _startPoint;
    private Stroke _lastStroke;
    private Stroke _topStroke;
    private Stroke _leftSideStroke;
    private Stroke _rightSideStroke;
    private Stroke _bottomStroke;
    private StrokeCollection _arrowheadCollection = [];
    private Shape _currentShape;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainInkCanvasControl"/> class.
    /// </summary>
    public MainInkCanvasControl()
    {
        InitializeComponent();
        MainInkCanvas.MouseLeftButtonDown += StartDrawing;
        MainInkCanvas.MouseLeftButtonUp += EndDrawing;
        MainInkCanvas.MouseMove += DrawShape;

        _strokeHistoryService = App.ServiceProvider.GetService<IStrokeHistoryService>();
        _inkEditingModeStateHandler = App.ServiceProvider.GetService<IInkEditingModeStateHandler>();
    }

    private static bool IsDrawingShapeKeyEntered(Key key) => key == Key.LeftShift || key == Key.RightShift;

    private void DrawShape(object sender, MouseEventArgs e)
    {
        var viewModel = (PaletteStateOrchestratorViewModel)DataContext;

        if (viewModel.DrawingViewModel.CurrentShape is DrawingShape.FreeDraw)
        {
            DrawLine(sender, e);
        }
        else if (viewModel.DrawingViewModel.CurrentShape is DrawingShape.Database)
        {
            DrawDatabaseShape(sender, e);
        }
        else if (viewModel.DrawingViewModel.CurrentShape is DrawingShape.Arrow)
        {
            DrawArrow(sender, e);
        }
        else if (viewModel.DrawingViewModel.CurrentShape is DrawingShape.Rectangle)
        {
            DrawRectangle(sender, e);
        }
        else if (viewModel.DrawingViewModel.CurrentShape is DrawingShape.Circle)
        {
            DrawCircle(sender, e);
        }
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (IsDrawingShapeKeyEntered(e.Key) && _inkEditingModeStateHandler.InkEditingMode is InkCanvasEditingMode.Ink)
        {
            _inkEditingModeStateHandler.None();
            MainInkCanvas.UseCustomCursor = true;
        }
    }

    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
        if (IsDrawingShapeKeyEntered(e.Key)
            && _inkEditingModeStateHandler.InkEditingMode is InkCanvasEditingMode.None
            && ((PaletteStateOrchestratorViewModel)DataContext).DrawingViewModel.CurrentShape is DrawingShape.FreeDraw)
        {
            _inkEditingModeStateHandler.Ink();
            MainInkCanvas.UseCustomCursor = false;
        }
    }

    private void StartDrawing(object sender, MouseButtonEventArgs e)
    {
        _isMoving = true;
        _startPoint = e.GetPosition(MainInkCanvas);
        _lastStroke = null;
        _topStroke = null;
        _arrowheadCollection = [];
        _strokeHistoryService.IgnoreStrokesChange = true;
    }

    private void EndDrawing(object sender, MouseButtonEventArgs e)
    {
        if (_isMoving is true)
        {
            var endPoint = e.GetPosition(MainInkCanvas);
            if (_lastStroke is not null)
            {
                StrokeCollection collection = [_lastStroke, .. _arrowheadCollection];
                _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType(collection));
            }
        }

        _currentShape = null;
        _isMoving = false;
        _strokeHistoryService.IgnoreStrokesChange = false;
    }

    private void DrawLine(object sender, MouseEventArgs e)
    {
        if (_isMoving is false)
        {
            return;
        }

        var newLine = MainInkCanvas.DefaultDrawingAttributes.Clone();
        newLine.StylusTip = StylusTip.Ellipse;
        newLine.IgnorePressure = true;

        var endPoint = e.GetPosition(MainInkCanvas);

        List<Point> pList = [
            new Point(_startPoint.X, _startPoint.Y),
            new Point(endPoint.X, endPoint.Y),
        ];

        var point = new StylusPointCollection(pList);
        var stroke = new Stroke(point)
        {
            DrawingAttributes = newLine,
        };

        if (_lastStroke is not null)
        {
            MainInkCanvas.Strokes.Remove(_lastStroke);
        }

        if (stroke is not null)
        {
            MainInkCanvas.Strokes.Add(stroke);
        }

        _lastStroke = stroke;
    }

    private void DrawCircle(object sender, MouseEventArgs e)
    {
        if (!_isMoving)
        {
            return;
        }

        var currentPoint = e.GetPosition(MainInkCanvas);

        // Calculate the center of the circle
        var centerX = (_startPoint.X + currentPoint.X) / 2;
        var centerY = (_startPoint.Y + currentPoint.Y) / 2;
        var center = new Point(centerX, centerY);

        var radius = Math.Sqrt(Math.Pow(currentPoint.X - centerX, 2) + Math.Pow(currentPoint.Y - centerY, 2));

        var points = new List<Point>();
        var segments = 100;
        for (var i = 0; i <= segments; i++)
        {
            var angle = 2 * Math.PI * i / segments;
            var x = center.X + (radius * Math.Cos(angle));
            var y = center.Y + (radius * Math.Sin(angle));
            points.Add(new Point(x, y));
        }

        var stylusPoints = new StylusPointCollection(points);
        var newAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone();
        newAttributes.StylusTip = StylusTip.Ellipse;
        newAttributes.IgnorePressure = true;

        var stroke = new Stroke(stylusPoints)
        {
            DrawingAttributes = newAttributes,
        };

        if (_lastStroke != null)
        {
            MainInkCanvas.Strokes.Remove(_lastStroke);
        }

        MainInkCanvas.Strokes.Add(stroke);
        _lastStroke = stroke;
    }

    private void DrawDatabaseShape(object sender, MouseEventArgs e)
    {
        if (!_isMoving)
        {
            return;
        }

        var currentPoint = e.GetPosition(MainInkCanvas);

        var width = Math.Abs(currentPoint.X - _startPoint.X);
        var height = Math.Abs(currentPoint.Y - _startPoint.Y);
        var radiusX = width / 2;
        var centerX = (_startPoint.X + currentPoint.X) / 2;
        var topY = _startPoint.Y;
        var bottomY = _startPoint.Y + height;

        var topEllipsePoints = GetEllipsePoints(centerX, topY, radiusX, true);
        var topStylusPoints = new StylusPointCollection(topEllipsePoints);
        var topStroke = new Stroke(topStylusPoints)
        {
            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
        };

        var leftSideStylusPoints = new StylusPointCollection
    {
        new StylusPoint(centerX - radiusX, topY),
        new StylusPoint(centerX - radiusX, bottomY),
    };
        var rightSideStylusPoints = new StylusPointCollection
    {
        new StylusPoint(centerX + radiusX, topY),
        new StylusPoint(centerX + radiusX, bottomY),
    };
        var leftSideStroke = new Stroke(leftSideStylusPoints)
        {
            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
        };
        var rightSideStroke = new Stroke(rightSideStylusPoints)
        {
            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
        };

        var bottomEllipsePoints = GetEllipsePoints(centerX, bottomY, radiusX, false);
        var bottomStylusPoints = new StylusPointCollection(bottomEllipsePoints);
        var bottomStroke = new Stroke(bottomStylusPoints)
        {
            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
        };

        bottomStroke.DrawingAttributes.StylusTip = StylusTip.Ellipse;

        if (_topStroke is not null)
        {
            MainInkCanvas.Strokes.Remove(_topStroke);
            MainInkCanvas.Strokes.Remove(_leftSideStroke);
            MainInkCanvas.Strokes.Remove(_rightSideStroke);
            MainInkCanvas.Strokes.Remove(_bottomStroke);
        }

        MainInkCanvas.Strokes.Add(topStroke);
        MainInkCanvas.Strokes.Add(leftSideStroke);
        MainInkCanvas.Strokes.Add(rightSideStroke);
        MainInkCanvas.Strokes.Add(bottomStroke);

        _topStroke = topStroke;
        _leftSideStroke = leftSideStroke;
        _rightSideStroke = rightSideStroke;
        _bottomStroke = bottomStroke;

        static IReadOnlyList<Point> GetEllipsePoints(double centerX, double centerY, double radiusX, bool isTop)
        {
            var segments = 100;
            List<Point> points = [];
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

    private void DrawRectangle(object sender, MouseEventArgs e)
    {
        if (_isMoving is false)
        {
            return;
        }

        var endPoint = e.GetPosition(MainInkCanvas);

        var topLeft = new Point(Math.Min(_startPoint.X, endPoint.X), Math.Min(_startPoint.Y, endPoint.Y));
        var topRight = new Point(Math.Max(_startPoint.X, endPoint.X), Math.Min(_startPoint.Y, endPoint.Y));
        var bottomLeft = new Point(Math.Min(_startPoint.X, endPoint.X), Math.Max(_startPoint.Y, endPoint.Y));
        var bottomRight = new Point(Math.Max(_startPoint.X, endPoint.X), Math.Max(_startPoint.Y, endPoint.Y));
        var points = new List<Point> { topLeft, topRight, bottomRight, bottomLeft, topLeft };

        var stylusPoints = new StylusPointCollection(points);
        var newAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone();
        newAttributes.StylusTip = StylusTip.Rectangle;
        newAttributes.IgnorePressure = true;
        var stroke = new Stroke(stylusPoints)
        {
            DrawingAttributes = newAttributes,
        };
        if (_lastStroke != null)
        {
            MainInkCanvas.Strokes.Remove(_lastStroke);
        }

        MainInkCanvas.Strokes.Add(stroke);
        _lastStroke = stroke;
    }

    private void DrawArrow(object sender, MouseEventArgs e)
    {
        if (_isMoving is false)
        {
            return;
        }

        if (_lastStroke is not null)
        {
            MainInkCanvas.Strokes.Remove(_lastStroke);
        }

        if (_arrowheadCollection.Count > 0)
        {
            foreach (var item in _arrowheadCollection)
            {
                MainInkCanvas.Strokes.Remove(item);
            }

            _arrowheadCollection.Clear();
        }

        var endPoint = e.GetPosition(MainInkCanvas);
        var lineAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone();
        lineAttributes.StylusTip = StylusTip.Ellipse;
        lineAttributes.IgnorePressure = true;
        List<Point> linePoints = [_startPoint, endPoint];
        var lineStroke = new Stroke(new StylusPointCollection(linePoints)) { DrawingAttributes = lineAttributes };

        MainInkCanvas.Strokes.Add(lineStroke);
        _lastStroke = lineStroke;

        DrawArrowheadLine(15);
        DrawArrowheadLine(-15);
        void DrawArrowheadLine(double rotation)
        {
            var arrowAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone();
            arrowAttributes.StylusTip = StylusTip.Ellipse;
            arrowAttributes.IgnorePressure = true;

            VectorX ps = new(_startPoint.X, _startPoint.Y);
            VectorX pe = new(endPoint.X, endPoint.Y);

            var arrowPoint = (((ps - pe) * 0.85f) + ps).ToPoint();
            List<Point> arrowPoints = [endPoint, arrowPoint];

            Matrix rotatingMatrix = new();
            rotatingMatrix.RotateAt(rotation, endPoint.X, endPoint.Y);

            var arrowStroke = new Stroke(new StylusPointCollection(arrowPoints)) { DrawingAttributes = arrowAttributes };
            arrowStroke.Transform(rotatingMatrix, false);
            MainInkCanvas.Strokes.Add(arrowStroke);
            _arrowheadCollection.Add(arrowStroke);
        }
    }
}
