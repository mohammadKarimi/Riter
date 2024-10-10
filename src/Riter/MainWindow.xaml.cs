using System.Windows.Interop;
using Riter.Core;
using Riter.Core.Extensions;
using Riter.Core.Interfaces;
using Riter.ViewModel;

namespace Riter;

/// <summary>
/// Initializes a new instance of the <see cref="MainWindow"/> class.
/// </summary>
public partial class MainWindow : Window
{
    private readonly Dictionary<int, (uint modifiers, uint key, Action callback)> _hotkies;
    private GlobalHotkeyManager _globalHotkeyManager;

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
        _hotkies = new Dictionary<int, (uint modifiers, uint key, Action callback)>
        {
               { 9000, (GlobalHotkeyManager.CTRL | GlobalHotkeyManager.SHIFT, 0x41, OnHotkey1Pressed) }, // CTRL + SHIFT + A
        };

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

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        _globalHotkeyManager = new GlobalHotkeyManager(this);
        foreach (var hotkey in _hotkies)
        {
            _globalHotkeyManager.RegisterHotkey(hotkey.Key, hotkey.Value.modifiers, hotkey.Value.key, hotkey.Value.callback);
        }
    }

    private void OnHotkey1Pressed()
    {
        MessageBox.Show("Hotkey CTRL + SHIFT + A pressed!");
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


    protected override void OnClosed(EventArgs e)
    {
        _globalHotkeyManager.Dispose(); // Clean up hotkey registrations
        base.OnClosed(e);
    }
}
