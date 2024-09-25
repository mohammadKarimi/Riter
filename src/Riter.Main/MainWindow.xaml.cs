using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Riter.Main.Core;
using Riter.Main.Core.Extensions;

namespace Riter.Main;

public partial class MainWindow : Window
{
    private readonly PalleteStateViewModel _pallateStateViewModel;
    private Point _lastMousePosition;
    private bool _isDragging;
    public MainWindow(PalleteStateViewModel pallateStateViewModel)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        _pallateStateViewModel = pallateStateViewModel;
        this.SetEventListeners()
            .SetTopMost()
            .SetDefaultColor()
            .SetBrushSize();
    }

    private void ShortcutKeyDown(object sender, KeyEventArgs e)
    {

    }

    /// <summary>
    /// Clear History and Clear Canvas Ink.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void TrashButton_Click(object sender, EventArgs e)
    {
        _pallateStateViewModel.ClearHistory();
        MainInkCanvas.Strokes.Clear();
    }
    private void EraserButton_Click(object sender, EventArgs e)
    {
        var s = MainInkCanvas.EraserShape.Height;
        MainInkCanvas.EraserShape = new EllipseStylusShape(s, s);
        MainInkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
    }

    /// <summary>
    /// Minimize The Window and all drawings.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;

    /// <summary>
    /// Save The draw if setting available then, Exit Application.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExitButton_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown(0);

    private void DrawButton_Click(object sender, RoutedEventArgs e)
    {
        MainInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
    }

    private void Palette_MouseDown(object sender, MouseButtonEventArgs e) => StartDrag();
    private void Palette_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDragging) return;
        var currentMousePosition = Mouse.GetPosition(this);
        var offset = currentMousePosition - _lastMousePosition;

        Canvas.SetTop(MainPallete, Canvas.GetTop(MainPallete) + offset.Y);
        Canvas.SetLeft(MainPallete, Canvas.GetLeft(MainPallete) + offset.X);

        _lastMousePosition = currentMousePosition;
    }
    private void Palette_MouseUp(object sender, MouseButtonEventArgs e)
        => EndDrag();
    private void Palette_MouseLeave(object sender, MouseEventArgs e)
        => EndDrag();
    private void StartDrag()
    {
        _lastMousePosition = Mouse.GetPosition(this);
        _isDragging = true;
        MainPallete.Background = new SolidColorBrush(Colors.Transparent);
    }
    private void EndDrag()
    {
        _isDragging = false;
        MainPallete.Background = null;
    }
}
