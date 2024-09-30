using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Riter.Main.Core.Extensions;
using Riter.Main.ViewModel;

namespace Riter.Main;

/// <summary>
/// Initializes a new instance of the <see cref="MainWindow"/> class.
/// </summary>
public partial class MainWindow : Window
{
    private Point _lastMousePosition;
    private bool _isDragging;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// Sets up the UI, binds the <see cref="PalleteStateViewModel"/> to the DataContext,
    /// and applies various UI configurations such as setting event listeners,
    /// making the window top-most, setting the default color, and brush size.
    /// </summary>
    /// <param name="pallateStateViewModel">
    /// The view model that manages the palette state, which is used to bind data to the UI.
    /// </param>
    public MainWindow(PalleteStateViewModel pallateStateViewModel)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        this.SetEventListeners()
            .SetTopMost(true)
            .SetDefaultColor()
            .SetBrushSize();
    }

    private void ShortcutKeyDown(object sender, KeyEventArgs e)
    {

    }

    /// <summary>
    /// Clear History and Clear Canvas Ink.
    /// </summary>
    /// <param name="sender">The source of the event, typically the Exit button.</param>
    /// <param name="e">The event data associated with the button click.</param>
    private void TrashButton_Click(object sender, EventArgs e)
    {
        MainInkCanvas.Strokes.Clear();
    }

    /// <summary>
    /// Minimize The Window and all drawings.
    /// </summary>
    /// <param name="sender">The source of the event, typically the Exit button.</param>
    /// <param name="e">The event data associated with the button click.</param>
    private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        => WindowState = WindowState.Minimized;

    /// <summary>
    /// Handles the click event for the Exit button.
    /// If the drawing settings are saved, the application will exit.
    /// </summary>
    /// <param name="sender">The source of the event, typically the Exit button.</param>
    /// <param name="e">The event data associated with the button click.</param>
    private void ExitButton_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown(0);

    private void Palette_MouseDown(object sender, MouseButtonEventArgs e) => StartDrag();

    private void Palette_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDragging)
        {
            return;
        }

        var currentMousePosition = Mouse.GetPosition(this);
        var offset = currentMousePosition - _lastMousePosition;

        Canvas.SetTop(MainPallete, Canvas.GetTop(MainPallete) + offset.Y);
        Canvas.SetLeft(MainPallete, Canvas.GetLeft(MainPallete) + offset.X);

        _lastMousePosition = currentMousePosition;
    }

    private void Palette_MouseUp(object sender, MouseButtonEventArgs e) => EndDrag();

    private void Palette_MouseLeave(object sender, MouseEventArgs e) => EndDrag();

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
