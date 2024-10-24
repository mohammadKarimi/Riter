using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core;
using Riter.Core.Interfaces;
using Riter.ViewModel.Handlers;

namespace Riter.ViewModel;

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel : BaseViewModel, INotifyPropertyChanged
{
    private readonly IDrawingHandler _drawingHandler;
    private readonly IStrokeVisibilityHandler _strokeVisibilityHandler;
    private readonly IStrokeHistoryService _strokeHistoryService;
    private readonly IBrushSettingsHandler _brushSettingsHandler;

    public event PropertyChangedEventHandler PropertyChanged;

    public bool IsReleased => _drawingHandler.IsReleased;

    public string ColorSelected => _brushSettingsHandler.InkColor;

    public DrawingAttributes InkDrawingAttributes => DrawingAttributesFactory.CreateDrawingAttributes(_brushSettingsHandler.InkColor, _brushSettingsHandler.SizeOfBrush, _drawingHandler.IsHighlighter);

    public double SizeOfBrush => _brushSettingsHandler.SizeOfBrush;

    public InkCanvasEditingMode InkEditingMode => _drawingHandler.InkEditingMode;

    public string ButtonSelectedName => _drawingHandler.ButtonSelectedName;

    public bool IsHideAll => _strokeVisibilityHandler.IsHideAll;

    public Visibility SettingPanelVisibility => _drawingHandler.SettingPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public override void HandleHotkey(HotKey hotKey)
    {
        switch (hotKey)
        {
            case HotKey.Drawing:
                _drawingHandler.StartDrawing();
                break;
            case HotKey.Erasing:
                _drawingHandler.StartErasing();
                break;
            default:
                break;
        }
    }

    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OnStateChanged(object sender, PropertyChangedEventArgs e)
        => OnPropertyChanged(e.PropertyName);
}

/// <summary>
/// ViewModel for contacting with PalleteState object and UI.
/// </summary>
public partial class PalleteStateViewModel
{
    public PalleteStateViewModel(
        IDrawingHandler drawingHandler,
        IStrokeHistoryService strokeHistoryService,
        IStrokeVisibilityHandler strokeVisibilityHandler,
        IBrushSettingsHandler brushSettingsHandler)
    {
        _drawingHandler = drawingHandler;
        _strokeVisibilityHandler = strokeVisibilityHandler;
        _brushSettingsHandler = brushSettingsHandler;

        _drawingHandler.PropertyChanged += OnStateChanged;
        _strokeVisibilityHandler.PropertyChanged += OnStateChanged;
        _brushSettingsHandler.PropertyChanged += OnStateChanged;

        _strokeHistoryService = strokeHistoryService;
        HideAllButtonCommand = new RelayCommand(_strokeVisibilityHandler.HideAll);

        ReleasedButtonCommand = new RelayCommand(_drawingHandler.Release);
        DrawingButtonCommand = new RelayCommand(_drawingHandler.StartDrawing);
        ErasingButtonCommand = new RelayCommand(_drawingHandler.StartErasing);
        UndoButtonCommand = new RelayCommand(() => _strokeHistoryService.Undo());
        RedoButtonCommand = new RelayCommand(() => _strokeHistoryService.Redo());
        SettingButtonCommand = new RelayCommand(_drawingHandler.ToggleSettingsPanel);
        TrashButtonCommand = new RelayCommand(() => _strokeHistoryService.Clear());
        SetInkColorButtonCommand = new RelayCommand<string>(_brushSettingsHandler.SetInkColor);
        SetSizeOfBrushCommand = new RelayCommand<string>(_brushSettingsHandler.SetSizeOfBrush);
        DrawingHighlighterCommand = new RelayCommand(_drawingHandler.EnableHighlighter);
    }

    public ICommand ReleasedButtonCommand { get; private set; }

    public ICommand DrawingButtonCommand { get; private set; }

    public ICommand ErasingButtonCommand { get; private set; }

    public ICommand HideAllButtonCommand { get; private set; }

    public ICommand SettingButtonCommand { get; private set; }

    public ICommand UndoButtonCommand { get; private set; }

    public ICommand RedoButtonCommand { get; private set; }

    public ICommand TrashButtonCommand { get; private set; }

    public ICommand SetInkColorButtonCommand { get; private set; }

    public ICommand SetSizeOfBrushCommand { get; private set; }

    public ICommand DrawingHighlighterCommand { get; private set; }
}
