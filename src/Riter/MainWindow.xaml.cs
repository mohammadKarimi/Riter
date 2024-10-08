using Riter.Core.Extensions;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter;

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
        _strokeHistoryService = strokeHistoryService;
        _strokeHistoryService.SetMainElementToRedoAndUndo(MainInkCanvasControl.MainInkCanvas);

        this.SetEventListeners()
            .EnableDragging(MainPallete)
            .SetTopMost(true)
            .SetDefaultColor()
            .SetBrushSize();
    }

    private IStrokeHistoryService _strokeHistoryService { get; }

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
        MainInkCanvasControl.MainInkCanvas.Strokes.Clear();
    }

    private void UndoButton_Click(object sender, RoutedEventArgs e) => _strokeHistoryService.Undo();

    private void RedoButton_Click(object sender, RoutedEventArgs e) => _strokeHistoryService.Redo();
}
