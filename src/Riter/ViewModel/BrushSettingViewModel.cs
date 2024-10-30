using Riter.Core.Enum;

namespace Riter.ViewModel;

public sealed class BrushSettingsViewModel : BaseViewModel
{
    private readonly IBrushSettingsStateHandler _brushSettingsHandler;

    public BrushSettingsViewModel(IBrushSettingsStateHandler brushSettingsHandler)
    {
        _brushSettingsHandler = brushSettingsHandler;
        _brushSettingsHandler.PropertyChanged += OnStateChanged;
    }

    public string ColorSelected => _brushSettingsHandler.InkColor;

    public double SizeOfBrush => _brushSettingsHandler.SizeOfBrush;

    public string InkColor => _brushSettingsHandler.InkColor;

    public ICommand SetInkColorWithHotKeyCommand => new RelayCommand<InkColor>(_brushSettingsHandler.SetInkColorWithHotKey);

    public ICommand SetInkColorCommand => new RelayCommand<string>(_brushSettingsHandler.SetInkColor);

    public ICommand SetSizeOfBrushCommand => new RelayCommand<string>(_brushSettingsHandler.SetSizeOfBrush);

    public ICommand SetSizeOfBrushWithHotKeyCommand => new RelayCommand<BrushSize>(_brushSettingsHandler.SetSizeOfBrushWithHotKey);
}
