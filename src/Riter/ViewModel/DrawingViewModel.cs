using System.Windows.Controls;

namespace Riter.ViewModel;
public sealed class DrawingViewModel : BaseViewModel
{
    private readonly IDrawingStateHandler _drawingHandler;

    public DrawingViewModel(IDrawingStateHandler drawingHandler)
    {
        _drawingHandler = drawingHandler;
        _drawingHandler.PropertyChanged += OnStateChanged;
    }

    public bool IsReleased => _drawingHandler.IsReleased;

    public bool IsHighlighter => _drawingHandler.IsHighlighter;

    public bool SettingPanelVisibility => _drawingHandler.SettingPanelVisibility;

    public string ButtonSelectedName => _drawingHandler.ButtonSelectedName;

    public ICommand StartDrawingCommand => new RelayCommand(_drawingHandler.StartDrawing);

    public ICommand StartErasingCommand => new RelayCommand(_drawingHandler.StartErasing);

    public ICommand ReleaseCommand => new RelayCommand(_drawingHandler.Release);

    public ICommand ToggleHighlighterCommand => new RelayCommand(_drawingHandler.EnableHighlighter);

    public ICommand ToggleSettingsPanelCommand => new RelayCommand(_drawingHandler.ToggleSettingsPanel);
}
