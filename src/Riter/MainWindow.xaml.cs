using System.Windows.Interop;
using System.Windows.Ink;
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
    private readonly IStrokeHistoryService _strokeHistoryService;
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
        MainInkCanvasControl.MainInkCanvas.Strokes.StrokesChanged += StrokesChanged;

        _hotkies = new Dictionary<int, (uint modifiers, uint key, Action callback)>
        {
               { 9000, (GlobalHotkeyManager.CTRL | GlobalHotkeyManager.SHIFT, 0x41, OnHotkey1Pressed) },
        };

        this.SetEventListeners()
            .EnableDragging(MainPallete)
            .SetTopMost(true)
            .SetDefaultColor()
            .SetBrushSize();
    }

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

    protected override void OnClosed(EventArgs e)
    {
        _globalHotkeyManager.Dispose(); // Clean up hotkey registrations
        base.OnClosed(e);
    }
}
