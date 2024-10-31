//using System.Windows.Controls;
//using System.Windows.Ink;
//using System.Windows.Media;
//using System.Windows.Shapes;
//using Microsoft.Extensions.DependencyInjection;
//using Riter.Core.Enum;
//using Riter.Core.Interfaces;
//using Riter.Services;
//using Riter.ViewModel;

//namespace Riter.Core.UI;

///// <summary>
///// Interaction logic for MainInkCanvasControl.xaml.
///// </summary>
//public partial class MainInkCanvasControl : UserControl
//{
//    private readonly IStrokeHistoryService _strokeHistoryService;
//    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler;
//    private readonly bool _lineMode = false;
//    private bool _isMoving = false;
//    private Point _startPoint;
//    private Stroke _lastStroke;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="MainInkCanvasControl"/> class.
//    /// </summary>
//    public MainInkCanvasControl()
//    {
//        InitializeComponent();
//        MainInkCanvas.MouseLeftButtonDown += StartLine;
//        MainInkCanvas.MouseLeftButtonUp += EndLine;
//        MainInkCanvas.MouseMove += MakeLine;
//        MainInkCanvas.PreviewMouseDown += MainInkCanvas_PreviewMouseDown;

//        _strokeHistoryService = App.ServiceProvider.GetService<IStrokeHistoryService>();
//        _inkEditingModeStateHandler = App.ServiceProvider.GetService<IInkEditingModeStateHandler>();
//    }

//    private void Window_KeyDown(object sender, KeyEventArgs e)
//    {
//        if ((e.Key == Key.LeftShift || e.Key == Key.RightShift) && _inkEditingModeStateHandler.InkEditingMode == InkCanvasEditingMode.Ink)
//        {
//            LineMode();
//        }
//    }

//    private void Window_KeyUp(object sender, KeyEventArgs e)
//    {
//        if ((e.Key == Key.LeftShift || e.Key == Key.RightShift) && _inkEditingModeStateHandler.InkEditingMode == InkCanvasEditingMode.None)
//        {
//            StrokeMode();
//        }
//    }

//    private void LineMode()
//    {
//        _inkEditingModeStateHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.None);
//        MainInkCanvas.UseCustomCursor = true;
//    }

//    private void StrokeMode()
//    {
//        _inkEditingModeStateHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.Ink);
//        MainInkCanvas.UseCustomCursor = false;
//    }

//    private void StartLine(object sender, MouseButtonEventArgs e)
//    {
//        _isMoving = true;
//        _startPoint = e.GetPosition(MainInkCanvas);
//        _lastStroke = null;
//        _strokeHistoryService.IgnoreStrokesChange = true;
//    }

//    private void EndLine(object sender, MouseButtonEventArgs e)
//    {
//        if (_isMoving == true)
//        {
//            var endPoint = e.GetPosition(MainInkCanvas);
//            if (_lastStroke != null)
//            {
//                StrokeCollection collection = [_lastStroke];
//                _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType(collection));
//            }
//        }

//        _isMoving = false;
//        _strokeHistoryService.IgnoreStrokesChange = false;
//    }

//    private void MakeLine(object sender, MouseEventArgs e)
//    {
//        if (_isMoving == false)
//        {
//            return;
//        }

//        var newLine = MainInkCanvas.DefaultDrawingAttributes.Clone();
//        newLine.StylusTip = StylusTip.Ellipse;
//        newLine.IgnorePressure = true;

//        var endPoint = e.GetPosition(MainInkCanvas);

//        List<Point> pList = [
//            new Point(_startPoint.X, _startPoint.Y),
//            new Point(endPoint.X, endPoint.Y),
//        ];

//        var point = new StylusPointCollection(pList);
//        var stroke = new Stroke(point)
//        {
//            DrawingAttributes = newLine,
//        };

//        if (_lastStroke != null)
//        {
//            MainInkCanvas.Strokes.Remove(_lastStroke);
//        }

//        if (stroke != null)
//        {
//            MainInkCanvas.Strokes.Add(stroke);
//        }

//        _lastStroke = stroke;
//    }

//    private void MainInkCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
//    {
//        var viewModel = (PaletteStateOrchestratorViewModel)DataContext;
//        switch (viewModel.DrawingShapeViewModel.CurrentShape)
//        {
//            case DrawingShape.Arrow:
//                DrawArrow(e.GetPosition(MainInkCanvas));
//                break;
//            case DrawingShape.Circle:
//                DrawCircle(e.GetPosition(MainInkCanvas));
//                break;
//            case DrawingShape.Square:
//                DrawSquare(e.GetPosition(MainInkCanvas));
//                break;
//            default:
//                break;
//        }
//    }

//    private void DrawArrow(Point startPoint)
//    {
//        // Implement arrow drawing logic here
//    }

//    private void DrawCircle(Point startPoint)
//    {
//        EllipseGeometry circle = new(startPoint, 50, 50);
//        Path path = new() { Data = circle, Stroke = Brushes.Black, StrokeThickness = 2 };
//        MainInkCanvas.Children.Add(path);
//    }

//    private void DrawSquare(Point startPoint)
//    {
//        RectangleGeometry square = new(new Rect(startPoint, new Size(50, 50)));
//        Path path = new() { Data = square, Stroke = Brushes.Black, StrokeThickness = 2 };
//        MainInkCanvas.Children.Add(path);
//    }

//}

using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter.Core.UI
{
    public partial class MainInkCanvasControl : UserControl
    {
        private readonly IStrokeHistoryService _strokeHistoryService;
        private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler;
        private bool _isMoving = false;
        private Point _startPoint;
        private Stroke _lastStroke;
        private Shape _currentShape; // New: Tracks the current shape being drawn (circle, square)

        public MainInkCanvasControl()
        {
            InitializeComponent();
            MainInkCanvas.MouseLeftButtonDown += StartDrawing;
            MainInkCanvas.MouseLeftButtonUp += EndDrawing;
            MainInkCanvas.MouseMove += DrawShape;
           // MainInkCanvas.PreviewMouseDown += MainInkCanvas_PreviewMouseDown;

            //MainInkCanvas.MouseLeftButtonDown += StartLine;
            //MainInkCanvas.MouseLeftButtonUp += EndLine;
            //MainInkCanvas.MouseMove += MakeLine;

            _strokeHistoryService = App.ServiceProvider.GetService<IStrokeHistoryService>();
            _inkEditingModeStateHandler = App.ServiceProvider.GetService<IInkEditingModeStateHandler>();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.LeftShift || e.Key == Key.RightShift) && _inkEditingModeStateHandler.InkEditingMode == InkCanvasEditingMode.Ink)
            {
                LineMode();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.LeftShift || e.Key == Key.RightShift) && _inkEditingModeStateHandler.InkEditingMode == InkCanvasEditingMode.None)
            {
                StrokeMode();
            }
        }

        private void StartDrawing(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (PaletteStateOrchestratorViewModel)DataContext;

            if (viewModel.DrawingShapeViewModel.CurrentShape == DrawingShape.FreeDraw)
            {
                // Use existing line-drawing functionality
                StartLine(sender, e);
            }
            else
            {
                // Start drawing shape (circle or square)
                _isMoving = true;
                _startPoint = e.GetPosition(MainInkCanvas);
                _currentShape = null;
                _strokeHistoryService.IgnoreStrokesChange = true;

                // Initialize selected shape
                switch (viewModel.DrawingShapeViewModel.CurrentShape)
                {
                    case DrawingShape.Circle:
                        _currentShape = new Ellipse
                        {
                            Stroke = Brushes.Red,
                            StrokeThickness = 5
                        };
                        break;
                    case DrawingShape.Square:
                        _currentShape = new Rectangle
                        {
                            Stroke = Brushes.Red,
                            StrokeThickness = 5
                        };
                        break;
                }

                if (_currentShape != null)
                {
                    Canvas.SetLeft(_currentShape, _startPoint.X);
                    Canvas.SetTop(_currentShape, _startPoint.Y);
                    MainInkCanvas.Children.Add(_currentShape);
                }
            }
        }

        private void DrawShape(object sender, MouseEventArgs e)
        {
            if (!_isMoving || _currentShape == null) return;

            var currentPoint = e.GetPosition(MainInkCanvas);
            double width = Math.Abs(currentPoint.X - _startPoint.X);
            double height = Math.Abs(currentPoint.Y - _startPoint.Y);

            _currentShape.Width = width;
            _currentShape.Height = height;

            // Position the shape correctly relative to the starting point
            Canvas.SetLeft(_currentShape, Math.Min(_startPoint.X, currentPoint.X));
            Canvas.SetTop(_currentShape, Math.Min(_startPoint.Y, currentPoint.Y));
        }

        private void EndDrawing(object sender, MouseButtonEventArgs e)
        {
            if (_currentShape != null)
            {
                // Finish shape drawing
                _isMoving = false;
                _currentShape = null;
                _strokeHistoryService.IgnoreStrokesChange = false;
            }
            else
            {
                // End line drawing
                EndLine(sender, e);
            }
        }

        private void MainInkCanvas_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (PaletteStateOrchestratorViewModel)DataContext;
            if (viewModel.DrawingShapeViewModel.CurrentShape == DrawingShape.Arrow)
            {
                DrawArrow(e.GetPosition(MainInkCanvas));
            }
        }

        private void DrawArrow(Point startPoint)
        {
            // Implement arrow drawing logic here
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
            if (_isMoving)
            {
                var endPoint = e.GetPosition(MainInkCanvas);
                if (_lastStroke != null)
                {
                    StrokeCollection collection = new() { _lastStroke };
                    _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType(collection));
                }
            }

            _isMoving = false;
            _strokeHistoryService.IgnoreStrokesChange = false;
        }

        private void MakeLine(object sender, MouseEventArgs e)
        {
            if (!_isMoving || _currentShape != null) return; // Only draw line if no shape is currently being drawn

            var newLine = MainInkCanvas.DefaultDrawingAttributes.Clone();
            newLine.StylusTip = StylusTip.Ellipse;
            newLine.IgnorePressure = true;

            var endPoint = e.GetPosition(MainInkCanvas);

            List<Point> pList = new()
            {
                new Point(_startPoint.X, _startPoint.Y),
                new Point(endPoint.X, endPoint.Y)
            };

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

        private void LineMode()
        {
            _inkEditingModeStateHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.None);
            MainInkCanvas.UseCustomCursor = true;
        }

        private void StrokeMode()
        {
            _inkEditingModeStateHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.Ink);
            MainInkCanvas.UseCustomCursor = false;
        }
    }
}
