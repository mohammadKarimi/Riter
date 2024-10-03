using System.Windows.Ink;
using Riter.Main.Core.Extensions;
using Riter.Main.Core.Interfaces;
using Riter.Main.ViewModel;

namespace Riter.Main;

/// <summary>
/// Initializes a new instance of the <see cref="MainWindow"/> class.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
    /// Sets up the UI, binds the <see cref="PalleteStateViewModel"/> to the DataContext,
    /// and applies various UI configurations such as setting event listeners,
    /// making the window top-most, setting the default color, and brush size.
    /// </summary>
    /// <param name="pallateStateViewModel">
    /// The view model that manages the palette state, which is used to bind data to the UI.
    /// </param>
    /// <param name="strokeHistoryService">
    /// Manage the history of drawing history.
    /// </param>
    public MainWindow(PalleteStateViewModel pallateStateViewModel, IStrokeHistoryService strokeHistoryService)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        this.SetEventListeners()
            .EnableDragging(MainPallete)
            .SetTopMost(true)
            .SetDefaultColor()
            .SetBrushSize();
        _strokeHistoryService = strokeHistoryService;
    }

    private IStrokeHistoryService _strokeHistoryService { get; }

    /// <summary>
    /// Handles the StrokesChanged event when the user draws on the InkCanvas.
    /// This method will be used to track and store stroke changes in a stack for history purposes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains the stroke collection that has changed.</param>
    private static void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {

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
        _strokeHistoryService.Clear();
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
}
