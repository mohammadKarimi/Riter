using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter.Core.UI;

///// <summary>
///// Interaction logic for MainInkCanvasControl.xaml.
///// </summary>
//public partial class MainInkCanvasControl_1 : UserControl
//{
//    private void DrawDatabaseShape(object sender, MouseEventArgs e)
//    {

//        var currentPoint = e.GetPosition(MainInkCanvas);

//        var width = Math.Abs(currentPoint.X - _startPoint.X);
//        var height = Math.Abs(currentPoint.Y - _startPoint.Y);
//        var radiusX = width / 2;
//        var centerX = (_startPoint.X + currentPoint.X) / 2;
//        var topY = _startPoint.Y;
//        var bottomY = _startPoint.Y + height;

//        var topEllipsePoints = GetEllipsePoints(centerX, topY, radiusX, true);
//        var topStylusPoints = new StylusPointCollection(topEllipsePoints);
//        var topStroke = new Stroke(topStylusPoints)
//        {
//            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
//        };

//        var leftSideStylusPoints = new StylusPointCollection
//    {
//        new StylusPoint(centerX - radiusX, topY),
//        new StylusPoint(centerX - radiusX, bottomY),
//    };
//        var rightSideStylusPoints = new StylusPointCollection
//    {
//        new StylusPoint(centerX + radiusX, topY),
//        new StylusPoint(centerX + radiusX, bottomY),
//    };
//        var leftSideStroke = new Stroke(leftSideStylusPoints)
//        {
//            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
//        };
//        var rightSideStroke = new Stroke(rightSideStylusPoints)
//        {
//            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
//        };

//        var bottomEllipsePoints = GetEllipsePoints(centerX, bottomY, radiusX, false);
//        var bottomStylusPoints = new StylusPointCollection(bottomEllipsePoints);
//        var bottomStroke = new Stroke(bottomStylusPoints)
//        {
//            DrawingAttributes = MainInkCanvas.DefaultDrawingAttributes.Clone(),
//        };

//        bottomStroke.DrawingAttributes.StylusTip = StylusTip.Ellipse;

//        if (_topStroke is not null)
//        {
//            MainInkCanvas.Strokes.Remove(_topStroke);
//            MainInkCanvas.Strokes.Remove(_leftSideStroke);
//            MainInkCanvas.Strokes.Remove(_rightSideStroke);
//            MainInkCanvas.Strokes.Remove(_bottomStroke);
//        }

//        MainInkCanvas.Strokes.Add(topStroke);
//        MainInkCanvas.Strokes.Add(leftSideStroke);
//        MainInkCanvas.Strokes.Add(rightSideStroke);
//        MainInkCanvas.Strokes.Add(bottomStroke);

//        _topStroke = topStroke;
//        _leftSideStroke = leftSideStroke;
//        _rightSideStroke = rightSideStroke;
//        _bottomStroke = bottomStroke;

//        static IReadOnlyList<Point> GetEllipsePoints(double centerX, double centerY, double radiusX, bool isTop)
//        {
//            var segments = 100;
//            List<Point> points = [];
//            var angleStep = Math.PI * 2 / segments;
//            for (var i = 0; i <= segments; i++)
//            {
//                var angle = angleStep * i;
//                var x = centerX + (radiusX * Math.Cos(angle));
//                var y = centerY + (radiusX / 2 * Math.Sin(angle));
//                if (!isTop && angle > Math.PI)
//                {
//                    continue;
//                }

//                points.Add(new Point(x, y));
//            }

//            return points;
//        }
//    }
//}


public partial class MainInkCanvasControl : UserControl
{
    private readonly IStrokeHistoryService _strokeHistoryService;
    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler;
    private readonly IDictionary<DrawingShape, IShapeDrawer> _shapeDrawers;
    private bool _isDrawing = false;
    private Point _startPoint;
    private Stroke _lastStroke;

    public MainInkCanvasControl()
    {
        InitializeComponent();
        _strokeHistoryService = App.ServiceProvider.GetService<IStrokeHistoryService>();
        _inkEditingModeStateHandler = App.ServiceProvider.GetService<IInkEditingModeStateHandler>();
        _shapeDrawers = App.ServiceProvider.GetService<IEnumerable<IShapeDrawer>>().ToDictionary(drawer => drawer.SupportedShape);

        MainInkCanvas.MouseLeftButtonDown += StartDrawing;
        MainInkCanvas.MouseLeftButtonUp += EndDrawing;
        MainInkCanvas.MouseMove += DrawShape;
    }

    private static bool IsDrawingShapeKeyEntered(Key key) => key == Key.LeftShift || key == Key.RightShift;

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
        _isDrawing = true;
        _startPoint = e.GetPosition(MainInkCanvas);
        _lastStroke = null;
        _strokeHistoryService.IgnoreStrokesChange = true;
    }

    private void EndDrawing(object sender, MouseButtonEventArgs e)
    {
        if (_isDrawing && _lastStroke != null)
            _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType([_lastStroke]));

        _isDrawing = false;
        _strokeHistoryService.IgnoreStrokesChange = false;
    }

    private void DrawShape(object sender, MouseEventArgs e)
    {
        if (!_isDrawing) return;

        var viewModel = (PaletteStateOrchestratorViewModel)DataContext;
        var currentShape = viewModel.DrawingViewModel.CurrentShape;

        if (_shapeDrawers.TryGetValue(currentShape, out var drawer))
        {
            var endPoint = e.GetPosition(MainInkCanvas);
            var stroke = drawer.DrawShape(MainInkCanvas, _startPoint, endPoint);

            if (_lastStroke != null)
                MainInkCanvas.Strokes.Remove(_lastStroke);

            MainInkCanvas.Strokes.Add(stroke);
            _lastStroke = stroke;
        }
    }
}
