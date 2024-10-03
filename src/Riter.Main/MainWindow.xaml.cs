using System.Windows.Ink;
using Riter.Main.Core;
using Riter.Main.Core.Extensions;
using Riter.Main.Core.Interfaces;
using Riter.Main.Services;
using Riter.Main.ViewModel;

namespace Riter.Main;

/// <summary>
/// Initializes a new instance of the <see cref="MainWindow"/> class.
/// </summary>
public partial class MainWindow : Window
{
    private bool _ignoreStrokesChange;

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
    public MainWindow(PalleteStateViewModel pallateStateViewModel)
    {
        InitializeComponent();
        DataContext = pallateStateViewModel;
        _strokeHistoryService = new StrokeHistoryService(MainInkCanvas);

        this.SetEventListeners()
            .EnableDragging(MainPallete)
            .SetTopMost(true)
            .SetDefaultColor()
            .SetBrushSize();
        MainInkCanvas.Strokes.StrokesChanged += StrokesChanged;
    }

    private IStrokeHistoryService _strokeHistoryService { get; }

    /// <summary>
    /// Handles the StrokesChanged event when the user draws on the InkCanvas.
    /// This method will be used to track and store stroke changes in a stack for history purposes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains the stroke collection that has changed.</param>
    private void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {
        if (_ignoreStrokesChange)
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
        _strokeHistoryService.ClearRedoHistory();
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

    private void UndoButton_Click(object sender, RoutedEventArgs e)
    {
        _ignoreStrokesChange = true;
        _strokeHistoryService.Undo();
        _ignoreStrokesChange = false;
    }

    private void RedoButton_Click(object sender, RoutedEventArgs e)
    {
        _ignoreStrokesChange = true;
        _strokeHistoryService.Redo();
        _ignoreStrokesChange = false;
    }
}
