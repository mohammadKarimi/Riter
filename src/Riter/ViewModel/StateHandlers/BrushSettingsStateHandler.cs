using Riter.Core.Enum;

namespace Riter.ViewModel.StateHandlers;

public class BrushSettingsStateHandler : BaseStateHandler, IBrushSettingsStateHandler
{
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler;
    private readonly ISettingPanelStateHandler _settingPanelStateHandler;
    private string _inkColor;
    private string _colorSelected;
    private double _sizeOfBrush;

    public BrushSettingsStateHandler(IButtonSelectedStateHandler buttonSelectedStateHandler, ISettingPanelStateHandler settingPanelStateHandler)
    {
        _buttonSelectedStateHandler = buttonSelectedStateHandler;
        _settingPanelStateHandler = settingPanelStateHandler;
        SetInkColor(AppSettings.InkDefaultColor);
        SizeOfBrush = AppSettings.BrushSize;
    }

    public string ColorSelected
    {
        get => _colorSelected;
        private set => SetProperty(ref _colorSelected, value, nameof(ColorSelected));
    }

    public string InkColor
    {
        get => _inkColor;
        private set => SetProperty(ref _inkColor, value, nameof(InkColor));
    }

    public double SizeOfBrush
    {
        get => _sizeOfBrush;
        private set => SetProperty(ref _sizeOfBrush, value, nameof(SizeOfBrush));
    }

    public void SetInkColor(string color)
    {
        InkColor = color;
        ColorSelected = color;
        _buttonSelectedStateHandler.ResetPreviousButton();
        _settingPanelStateHandler.SetSettingPanelInvisibile();
    }

    public void SetSizeOfBrush(string size)
    {
        SizeOfBrush = double.Parse(size);
        _buttonSelectedStateHandler.ResetPreviousButton();
        _settingPanelStateHandler.SetSettingPanelInvisibile();
    }

    public void SetSizeOfBrushWithHotKey(BrushSize size) => SizeOfBrush = (double)size;
}
