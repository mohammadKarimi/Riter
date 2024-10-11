using System.ComponentModel;
using System.Windows.Controls;
using Riter.Core;
using Riter.Core.Interfaces;

namespace Riter.ViewModel;

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel : INotifyPropertyChanged
{
    private readonly PalleteState _state;
    private readonly IStrokeHistoryService _strokeHistoryService;

    /// <summary>
    /// This event is for subscribing the UI on it to get any state changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets a value indicating whether the ink has been released.
    /// </summary>
    public bool IsReleased => _state.IsReleased;

    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    public InkCanvasEditingMode InkEditingMode => _state.InkEditingMode;

    /// <summary>
    /// Gets the name of the button that is currently selected.
    /// </summary>
    public string ButtonSelectedName => _state.ButtonSelectedName;

    /// <summary>
    /// Gets a value indicating whether gets the value of IsHideAll props to show or hide the strokes.
    /// </summary>
    public bool IsHideAll => _state.IsHideAll;

    /// <summary>
    /// Gets a value indicating whether gets the value of IsHideAll props to show or hide the strokes.
    /// </summary>
    public Visibility SettingPanelVisibility => _state.SettingPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    /// <summary>
    /// Decides which method to call based on the hotkey pressed.
    /// </summary>
    /// <param name="hotKey">The hotkey that was pressed.</param>
    public void HandleHotkey(HotKey hotKey)
    {
        switch (hotKey)
        {
            case HotKey.CTRL_R:
                _state.Release();
                break;
            case HotKey.CTRL_H:
                _state.HideAll();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Raises the PropertyChanged event when a property value changes.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    /// <summary>
    /// Handles the state change event and raises the PropertyChanged event.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Property changed event arguments.</param>
    private void OnStateChanged(object sender, PropertyChangedEventArgs e)
        => OnPropertyChanged(e.PropertyName);

    /// <summary>
    /// Reverts the last stroke action (undo) if possible.
    /// Removes the last stroke from the history and applies the change.
    /// </summary>
    private void Undo() => _strokeHistoryService.Undo();

    /// <summary>
    /// Reapplies the last undone stroke action (redo) if possible.
    /// Re-adds the previously undone stroke to the canvas.
    /// </summary>
    private void Redo() => _strokeHistoryService.Redo();

    /// <summary>
    /// Clear History and Clear Canvas Ink.
    /// </summary>
    private void Trash() => _strokeHistoryService.Clear();
}

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PalleteStateViewModel"/> class.
    /// </summary>
    /// <param name="palleteState">The PalleteState model used to manage ink states.</param>
    /// <param name="strokeHistoryService">stroke History service for undo and redo.</param>
    public PalleteStateViewModel(PalleteState palleteState, IStrokeHistoryService strokeHistoryService)
    {
        _state = palleteState;
        _state.PropertyChanged += OnStateChanged;
        _strokeHistoryService = strokeHistoryService;

        ReleasedButtonCommand = new RelayCommand(_state.Release);
        DrawingButtonCommand = new RelayCommand(_state.StartDrawing);
        ErasingButtonCommand = new RelayCommand(_state.StartErasing);
        UndoButtonCommand = new RelayCommand(Undo);
        RedoButtonCommand = new RelayCommand(Redo);
        HideAllButtonCommand = new RelayCommand(_state.HideAll);
        SettingButtonCommand = new RelayCommand(_state.ToggleSettingsPanel);
        TrashButtonCommand = new RelayCommand(Trash);
    }

    /// <summary>
    /// Gets the command that is executed when the released button is pressed.
    /// </summary>
    public ICommand ReleasedButtonCommand { get; private set; }

    /// <summary>
    /// Gets the command that is executed when the drawing button is pressed.
    /// </summary>
    public ICommand DrawingButtonCommand { get; private set; }

    /// <summary>
    /// Gets the command that is executed when the erasing button is pressed.
    /// </summary>
    public ICommand ErasingButtonCommand { get; private set; }

    /// <summary>
    /// Gets HideAll Strokes in MainInk.
    /// </summary>
    public ICommand HideAllButtonCommand { get; private set; }

    /// <summary>
    /// Gets SettingButton.
    /// </summary>
    public ICommand SettingButtonCommand { get; private set; }

    /// <summary>
    /// Gets Undo.
    /// </summary>
    public ICommand UndoButtonCommand { get; private set; }

    /// <summary>
    /// Gets Redo.
    /// </summary>
    public ICommand RedoButtonCommand { get; private set; }

    /// <summary>
    /// Gets click on Trash button to clear strokes.
    /// </summary>
    public ICommand TrashButtonCommand { get; private set; }
}
