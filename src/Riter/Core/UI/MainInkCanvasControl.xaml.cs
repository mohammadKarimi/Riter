using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Riter.Core.Consts;
using Riter.Core.Drawing;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter.Core.UI;

public partial class MainInkCanvasControl : UserControl
{
    private IStrokeHistoryService _strokeHistoryService;
    private IInkEditingModeStateHandler _inkEditingModeStateHandler;
    private Dictionary<DrawingShape, IShapeDrawer> _shapeDrawers;
    private bool _isDrawing = false;
    private bool _isDragging = false;
    private Point _startPoint;
    private Stroke _lastStroke;
    private Stroke _selectedStroke;
    private Point _startDragPoint;

    public MainInkCanvasControl()
    {
        InitializeComponent();

        DataContextChanged += MainInkCanvasControl_DataContextChanged;
        MainInkCanvas.Strokes.StrokesChanged += StrokesChanged;
        MainInkCanvas.MouseLeftButtonDown += StartDrawing;
        MainInkCanvas.MouseLeftButtonUp += EndDrawing;
        MainInkCanvas.MouseMove += DrawShape;
    }

    private static bool IsDrawingShapeKeyEntered(Key key) => key == Key.LeftShift || key == Key.RightShift;

    /// <summary>
    /// Creates a translation matrix for dragging strokes.
    /// </summary>
    /// <param name="startPoint">The initial point.</param>
    /// <param name="endPoint">The current point.</param>
    /// <returns>A translation matrix representing the movement.</returns>
    private static Matrix CreateTranslationMatrix(Point startPoint, Point endPoint)
    {
        var dx = endPoint.X - startPoint.X;
        var dy = endPoint.Y - startPoint.Y;
        var translationMatrix = default(Matrix);
        translationMatrix.Translate(dx, dy);
        return translationMatrix;
    }

    /// <summary>
    /// Checks if the currently selected button is either the drawing or highlighter button.
    /// </summary>
    /// <param name="viewModel">The current palette state view model.</param>
    /// <returns>True if the drawing or highlighter button is selected, otherwise false.</returns>
    private static bool IsDrawingOrHighlighterButtonSelected(PaletteStateOrchestratorViewModel viewModel)
    {
        var selectedButton = viewModel.ButtonSelectedViewModel.ButtonSelectedName;
        return selectedButton == ButtonNames.DrawingButton || selectedButton == ButtonNames.HighlighterButton;
    }

    /// <summary>
    /// Resolves the actual key pressed, accounting for the System key.
    /// </summary>
    /// <param name="e">Key event arguments.</param>
    /// <returns>The resolved key.</returns>
    private static Key ResolveActualKey(KeyEventArgs e) => e.Key == Key.System ? e.SystemKey : e.Key;

    /// <summary>
    /// Checks if an Alt key (LeftAlt or RightAlt) was released.
    /// </summary>
    /// <param name="key">The actual key released.</param>
    /// <returns>True if an Alt key was released, otherwise false.</returns>
    private static bool IsAltKeyReleased(Key key) => key == Key.LeftAlt || key == Key.RightAlt;

    private void MainInkCanvasControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is PaletteStateOrchestratorViewModel viewModel)
        {
            viewModel.InkEditingModeViewModel.PropertyChanged += DrawingViewModel_PropertyChanged;
            viewModel.DrawingViewModel.PropertyChanged += DrawingViewModel_PropertyChanged;

            _strokeHistoryService = viewModel.InkCanvasViewModel.StrokeHistoryService;
            _inkEditingModeStateHandler = viewModel.InkCanvasViewModel.InkEditingModeStateHandler;
            _shapeDrawers = viewModel.InkCanvasViewModel.ShapeDrawer.ToDictionary(drawer => drawer.SupportedShape);
            _strokeHistoryService.SetMainElementToRedoAndUndo(MainInkCanvas);
        }
    }

    private void DrawingViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        var viewModel = (PaletteStateOrchestratorViewModel)DataContext;

        if (e.PropertyName == nameof(viewModel.InkEditingModeViewModel.InkEditingMode)
          && viewModel.InkEditingModeViewModel.InkEditingMode is InkCanvasEditingMode.None)
        {
            MainInkCanvas.UseCustomCursor = true;
        }

        if (e.PropertyName == nameof(viewModel.DrawingViewModel.CurrentShape))
        {
            MainInkCanvas.Cursor = CursorFactory.Create(viewModel.DrawingViewModel.CurrentShape);
        }

        if (e.PropertyName == nameof(viewModel.InkEditingModeViewModel.InkEditingMode)
            && viewModel.InkEditingModeViewModel.InkEditingMode is InkCanvasEditingMode.Ink)
        {
            MainInkCanvas.UseCustomCursor = false;
        }
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
        {
            _isDragging = true;
            _inkEditingModeStateHandler.None();
            MainInkCanvas.UseCustomCursor = true;
            MainInkCanvas.Cursor = CursorFactory.CreateMoveCursor();
        }

        if (IsDrawingShapeKeyEntered(e.Key) && _inkEditingModeStateHandler.InkEditingMode is InkCanvasEditingMode.Ink)
        {
            _inkEditingModeStateHandler.None();
            MainInkCanvas.UseCustomCursor = true;
            MainInkCanvas.Cursor = CursorFactory.Create(DrawingShape.Line);
        }
    }

    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
        var viewModel = (PaletteStateOrchestratorViewModel)DataContext;
        var actualKey = ResolveActualKey(e);

        if (IsAltKeyReleased(actualKey))
        {
            MainInkCanvas.Cursor = CursorFactory.Create(viewModel.DrawingViewModel.CurrentShape);
        }

        if ((IsAltKeyReleased(actualKey) && viewModel.ButtonSelectedViewModel.ButtonSelectedName == ButtonNames.DrawingButton) || IsDrawingKeyReleased(e, viewModel))
        {
            _inkEditingModeStateHandler.Ink();
            MainInkCanvas.UseCustomCursor = false;
        }
    }

    /// <summary>
    /// Checks if a drawing shape key was released and the current state allows for switching back to Ink mode.
    /// </summary>
    /// <param name="e">Key event arguments.</param>
    /// <param name="viewModel">The current palette state view model.</param>
    /// <returns>True if the drawing shape key was released and conditions are met, otherwise false.</returns>
    private bool IsDrawingKeyReleased(KeyEventArgs e, PaletteStateOrchestratorViewModel viewModel)
        => IsDrawingShapeKeyEntered(e.Key) &&
               _inkEditingModeStateHandler.InkEditingMode == InkCanvasEditingMode.None &&
               IsDrawingOrHighlighterButtonSelected(viewModel);

    private void StartDrawing(object sender, MouseButtonEventArgs e)
    {
        if (((PaletteStateOrchestratorViewModel)DataContext).DrawingViewModel.IsReleased)
            return;

        _startDragPoint = e.GetPosition(MainInkCanvas);
        _selectedStroke = GetStrokeUnderCursor(_startDragPoint);

        if (_selectedStroke is not null)
        {
            MainInkCanvas.CaptureMouse();
        }

        _isDrawing = true;
        _startPoint = e.GetPosition(MainInkCanvas);
        _lastStroke = null;
        _strokeHistoryService.IgnoreStrokesChange = true;
    }

    private void EndDrawing(object sender, MouseButtonEventArgs e)
    {
        if (_selectedStroke is not null)
        {
            MainInkCanvas.ReleaseMouseCapture();
            _selectedStroke = null;
        }

        if (_isDrawing && _lastStroke is not null)
            _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType([_lastStroke]));

        _isDrawing = false;
        _isDragging = false;
        _strokeHistoryService.IgnoreStrokesChange = false;
    }

    private Stroke GetStrokeUnderCursor(Point position)
    {
        foreach (var stroke in MainInkCanvas.Strokes)
        {
            if (stroke.HitTest(position, 2.0))
            {
                return stroke;
            }
        }

        return null;
    }

    private void DrawShape(object sender, MouseEventArgs e)
    {
        if (HandleDragging(e)) return;

        if (!ShouldDraw()) return;

        var (viewModel, currentShape) = GetCurrentShape();
        if (!_shapeDrawers.TryGetValue(currentShape, out var drawer)) return;

        var endPoint = e.GetPosition(MainInkCanvas);
        DrawAndReplaceShape(drawer, viewModel, endPoint);
    }

    /// <summary>
    /// Handles dragging logic when a stroke is being moved.
    /// </summary>
    /// <param name="e">Mouse event arguments.</param>
    /// <returns>True if dragging was handled, otherwise false.</returns>
    private bool HandleDragging(MouseEventArgs e)
    {
        if (!_isDragging || _selectedStroke == null || e.LeftButton != MouseButtonState.Pressed)
            return false;

        var currentPosition = e.GetPosition(MainInkCanvas);
        var translationMatrix = CreateTranslationMatrix(_startDragPoint, currentPosition);
        _selectedStroke.Transform(translationMatrix, false);
        _startDragPoint = currentPosition;

        return true;
    }

    /// <summary>
    /// Determines whether drawing should proceed.
    /// </summary>
    /// <returns>True if drawing should continue, otherwise false.</returns>
    private bool ShouldDraw() => !_isDragging && _isDrawing;

    /// <summary>
    /// Draws a new shape and replaces the last drawn shape.
    /// </summary>
    /// <param name="drawer">The shape drawer to use.</param>
    /// <param name="viewModel">The current palette state view model.</param>
    /// <param name="endPoint">The endpoint of the shape.</param>
    private void DrawAndReplaceShape(IShapeDrawer drawer, PaletteStateOrchestratorViewModel viewModel, Point endPoint)
    {
        var stroke = drawer.DrawShape(MainInkCanvas, _startPoint, endPoint, viewModel.BrushSettingsViewModel.IsRainbow);

        if (_lastStroke != null)
            MainInkCanvas.Strokes.Remove(_lastStroke);

        MainInkCanvas.Strokes.Add(stroke);
        _lastStroke = stroke;
    }

    /// <summary>
    /// Gets the current shape and view model.
    /// </summary>
    /// <returns>A tuple containing the palette state view model and the current drawing shape.</returns>
    private (PaletteStateOrchestratorViewModel viewModel, DrawingShape currentShape) GetCurrentShape()
    {
        var viewModel = (PaletteStateOrchestratorViewModel)DataContext;
        var currentShape = viewModel.DrawingViewModel.CurrentShape;

        if (viewModel.ButtonSelectedViewModel.ButtonSelectedName == ButtonNames.DrawingButton ||
            viewModel.ButtonSelectedViewModel.ButtonSelectedName == ButtonNames.HighlighterButton)
        {
            currentShape = DrawingShape.Line;
        }

        return (viewModel, currentShape);
    }

    /// <summary>
    /// Handles the StrokesChanged event when the user draws on the InkCanvas.
    /// This method will be used to track and store stroke changes in a stack for history purposes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains the stroke collection that has changed.</param>
    private void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {
        if (_strokeHistoryService.IgnoreStrokesChange)
        {
            return;
        }

        if (e.Added.Count != 0)
        {
            _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType(e.Added));
        }

        if (e.Removed.Count != 0)
        {
            _strokeHistoryService.Push(StrokesHistoryNode.CreateRemovedType(e.Removed));
        }
    }
}
