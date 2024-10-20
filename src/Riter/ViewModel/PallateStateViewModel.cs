using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using Riter.Core;
using Riter.Core.Interfaces;
using Riter.ViewModel.Handlers;

namespace Riter.ViewModel;

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel : INotifyPropertyChanged
{
    private readonly IButtonSelectionHandler _buttonSelectionHandler;
    private readonly IStrokeVisibilityHandler _strokeVisibilityHandler;
    private readonly IStrokeHistoryService _strokeHistoryService;
    private readonly IBrushSettingsHandler _brushSettingsHandler;

    /// <summary>
    /// This event is for subscribing the UI on it to get any state changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets a value indicating whether the ink has been released.
    /// </summary>
    public bool IsReleased => _buttonSelectionHandler.IsReleased;

    /// <summary>
    /// Gets a value Of Ink Color which User selected.
    /// </summary>
    public string ColorSelected => _brushSettingsHandler.InkColor;

    /// <summary>
    /// Gets a value Of Ink Color which User selected.
    /// </summary>
    public DrawingAttributes InkDrawingAttributes => DrawingAttributesFactory.CreateDrawingAttributes(_brushSettingsHandler.InkColor, _brushSettingsHandler.SizeOfBrush, _buttonSelectionHandler.IsHighlighter);

    /// <summary>
    /// Gets a value Of Ink Color which User selected.
    /// </summary>
    public double SizeOfBrush => _brushSettingsHandler.SizeOfBrush;

    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    public InkCanvasEditingMode InkEditingMode => _buttonSelectionHandler.InkEditingMode;

    /// <summary>
    /// Gets the name of the button that is currently selected.
    /// </summary>
    public string ButtonSelectedName => _buttonSelectionHandler.ButtonSelectedName;

    /// <summary>
    /// Gets a value indicating whether gets the value of IsHideAll props to show or hide the strokes.
    /// </summary>
    public bool IsHideAll => _strokeVisibilityHandler.IsHideAll;

    /// <summary>
    /// Gets a value indicating whether gets the value of IsHideAll props to show or hide the strokes.
    /// </summary>
    public Visibility SettingPanelVisibility => _buttonSelectionHandler.SettingPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    /// <summary>
    /// Decides which method to call based on the hotkey pressed.
    /// </summary>
    /// <param name="hotKey">The hotkey that was pressed.</param>
    public void HandleHotkey(HotKey hotKey)
    {
        switch (hotKey)
        {
            case HotKey.CTRL_R:
                _buttonSelectionHandler.Release();
                break;
            case HotKey.CTRL_H:
                _strokeVisibilityHandler.HideAll();
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
}

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel
{
    public PalleteStateViewModel(IButtonSelectionHandler buttonSelectionHandler,
                                 IStrokeHistoryService strokeHistoryService,
                                 IStrokeVisibilityHandler strokeVisibilityHandler,
                                 IBrushSettingsHandler brushSettingsHandler)
    {
        _buttonSelectionHandler = buttonSelectionHandler;
        _strokeVisibilityHandler = strokeVisibilityHandler;
        _brushSettingsHandler = brushSettingsHandler;

        _buttonSelectionHandler.PropertyChanged += OnStateChanged;
        _strokeVisibilityHandler.PropertyChanged += OnStateChanged;
        _brushSettingsHandler.PropertyChanged += OnStateChanged;

        _strokeHistoryService = strokeHistoryService;
        HideAllButtonCommand = new RelayCommand(_strokeVisibilityHandler.HideAll);

        ReleasedButtonCommand = new RelayCommand(_buttonSelectionHandler.Release);
        DrawingButtonCommand = new RelayCommand(_buttonSelectionHandler.StartDrawing);
        ErasingButtonCommand = new RelayCommand(_buttonSelectionHandler.StartErasing);
        UndoButtonCommand = new RelayCommand(() => _strokeHistoryService.Undo());
        RedoButtonCommand = new RelayCommand(() => _strokeHistoryService.Redo());
        SettingButtonCommand = new RelayCommand(_buttonSelectionHandler.ToggleSettingsPanel);
        TrashButtonCommand = new RelayCommand(() => _strokeHistoryService.Clear());
        SetInkColorButtonCommand = new RelayCommand<string>(_brushSettingsHandler.SetInkColor);
        SetSizeOfBrushCommand = new RelayCommand<string>(_brushSettingsHandler.SetSizeOfBrush);
        DrawingHighlighterCommand = new RelayCommand(_buttonSelectionHandler.EnableHighlighter);
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

    /// <summary>
    /// Gets click on color pallete buttons in setting pannel.
    /// </summary>
    public ICommand SetInkColorButtonCommand { get; private set; }

    /// <summary>
    /// Gets click on brush size buttons in setting pannel.
    /// </summary>
    public ICommand SetSizeOfBrushCommand { get; private set; }

    public ICommand DrawingHighlighterCommand { get; private set; }
}
