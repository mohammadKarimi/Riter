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
    private readonly bool _lineMode = false;
    private bool _isMoving = false;
    private Point _startPoint;
    private Stroke _lastStroke;
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

        if (viewModel.DrawingShapeViewModel.CurrentShape == DrawingShape.FreeDraw)
        {
            DrawLine(sender, e);
        }
        else if (viewModel.DrawingShapeViewModel.CurrentShape == DrawingShape.Circle)
        {
            DrawArrow(sender, e);
        }
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (IsDrawingShapeKeyEntered(e.Key) && _inkEditingModeStateHandler.InkEditingMode == InkCanvasEditingMode.Ink)
        {
            _inkEditingModeStateHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.None);
            MainInkCanvas.UseCustomCursor = true;
        }
    }

    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
        if (IsDrawingShapeKeyEntered(e.Key) && _inkEditingModeStateHandler.InkEditingMode == InkCanvasEditingMode.None)
        {
            _inkEditingModeStateHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.Ink);
            MainInkCanvas.UseCustomCursor = false;
        }
    }

    private void StartDrawing(object sender, MouseButtonEventArgs e)
    {
        _isMoving = true;
        _startPoint = e.GetPosition(MainInkCanvas);
        _lastStroke = null;
        _arrowheadCollection = [];
        _strokeHistoryService.IgnoreStrokesChange = true;
    }

    private void EndDrawing(object sender, MouseButtonEventArgs e)
    {
        if (_isMoving == true)
        {
            var endPoint = e.GetPosition(MainInkCanvas);
            if (_lastStroke != null)
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
        if (_isMoving == false)
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

        if (_lastStroke != null)
        {
            MainInkCanvas.Strokes.Remove(_lastStroke);
        }

        if (stroke != null)
        {
            MainInkCanvas.Strokes.Add(stroke);
        }

        _lastStroke = stroke;
    }

    private void DrawCircle(object sender, MouseEventArgs e)
    {
        if (_isMoving == false)
        {
            return;
        }

        var currentPoint = e.GetPosition(MainInkCanvas);
        var radius = Math.Abs(currentPoint.X - _startPoint.X);
        var points = new List<Point>();

        var segments = 100;
        for (var i = 0; i <= segments; i++)
        {
            var angle = 2 * Math.PI * i / segments;
            var x = _startPoint.X + (radius * Math.Cos(angle));
            var y = _startPoint.Y + (radius * Math.Sin(angle));
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

    private void DrawRectangle(object sender, MouseEventArgs e)
    {
        if (!_isMoving)
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
        if (!_isMoving)
        {
            return;
        }

        if (_lastStroke != null)
        {
            MainInkCanvas.Strokes.Remove(_lastStroke);
        }

        if (_arrowheadCollection.Count > 0)
        {
            foreach (var item in _arrowheadCollection)
            {
                MainInkCanvas.Strokes.Remove(item);
            }
        }

        var endPoint = e.GetPosition(MainInkCanvas);
        var lineAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone();
        lineAttributes.StylusTip = StylusTip.Ellipse;
        lineAttributes.IgnorePressure = true;
        List<Point> linePoints = [_startPoint, endPoint];
        var lineStroke = new Stroke(new StylusPointCollection(linePoints)) { DrawingAttributes = lineAttributes };

        MainInkCanvas.Strokes.Add(lineStroke);
        _lastStroke = lineStroke;

        DrawArrowheadLine(15);  // Rotate 15 degrees for the right arrowhead line
        DrawArrowheadLine(-15); // Rotate -15 degrees for the left arrowhead line
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

            // Create the stroke for the arrowhead line
            var arrowStroke = new Stroke(new StylusPointCollection(arrowPoints)) { DrawingAttributes = arrowAttributes };
            arrowStroke.Transform(rotatingMatrix, false);
            MainInkCanvas.Strokes.Add(arrowStroke);
            _arrowheadCollection.Add(arrowStroke);
        }
    }
}
