using System.Windows.Controls;
using System.Windows.Ink;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter.Core.UI;

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
