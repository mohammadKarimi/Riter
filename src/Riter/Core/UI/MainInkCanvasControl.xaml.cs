using System.Windows.Controls;
using System.Windows.Ink;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter.Core.UI;

/// <summary>
/// Interaction logic for MainInkCanvasControl.xaml.
/// </summary>
public partial class MainInkCanvasControl : UserControl
{
    private readonly IStrokeHistoryService _strokeHistoryService;
    private readonly IDrawingStateHandler _drawingHandler;
    private readonly bool _lineMode = false;
    private bool _isMoving = false;
    private Point _startPoint;
    private Stroke _lastStroke;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainInkCanvasControl"/> class.
    /// </summary>
    public MainInkCanvasControl()
    {
        InitializeComponent();
        MainInkCanvas.MouseLeftButtonDown += StartLine;
        MainInkCanvas.MouseLeftButtonUp += EndLine;
        MainInkCanvas.MouseMove += MakeLine;
        _strokeHistoryService = App.ServiceProvider.GetService<IStrokeHistoryService>();
        _drawingHandler = App.ServiceProvider.GetService<IDrawingStateHandler>();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if ((e.Key == Key.LeftShift || e.Key == Key.RightShift) && _drawingHandler.InkEditingMode == InkCanvasEditingMode.Ink)
        {
            LineMode();
        }
    }

    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
        if ((e.Key == Key.LeftShift || e.Key == Key.RightShift) && _drawingHandler.InkEditingMode == InkCanvasEditingMode.None)
        {
            StrokeMode();
        }
    }

    private void LineMode()
    {
        _drawingHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.None);
        MainInkCanvas.UseCustomCursor = true;
    }

    private void StrokeMode()
    {
        _drawingHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.Ink);
        MainInkCanvas.UseCustomCursor = false;
    }

    private void StartLine(object sender, MouseButtonEventArgs e)
    {
        _isMoving = true;
        _startPoint = e.GetPosition(MainInkCanvas);
        _lastStroke = null;
        _strokeHistoryService.IgnoreStrokesChange = true;
    }

    private void EndLine(object sender, MouseButtonEventArgs e)
    {
        if (_isMoving == true)
        {
            var endPoint = e.GetPosition(MainInkCanvas);
            if (_lastStroke != null)
            {
                StrokeCollection collection = [_lastStroke];
                _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType(collection));
            }
        }

        _isMoving = false;
        _strokeHistoryService.IgnoreStrokesChange = false;
    }

    private void MakeLine(object sender, MouseEventArgs e)
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
}
