using Riter.Core.Drawing;
using Riter.Core.Enum;
using EnumInkColor = Riter.Core.Enum.InkColor;

namespace Riter.ViewModel.StateHandlers;

public class BrushSettingsStateHandler : BaseStateHandler, IBrushSettingsStateHandler
{
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler;
    private readonly ISettingPanelStateHandler _settingPanelStateHandler;
    private string _inkColor;
    private string _colorSelected;
    private double _sizeOfBrush;
    private bool _isRainbow;

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

    public bool IsRainbow
    {
        get => _isRainbow;
        private set => SetProperty(ref _isRainbow, value, nameof(IsRainbow));
    }

    public void SetInkColor(string color)
    {
        IsRainbow = color == EnumInkColor.RainBow.ToString();

        InkColor = color;
        ColorSelected = color;
        ResetSettings();
    }

    public void SetInkColorWithHotKey(EnumInkColor color)
    {
        if (color == EnumInkColor.RainBow)
        {
            IsRainbow = true;
            InkColor = ColorSelected = color.ToString();
        }
        else
        {
            IsRainbow = false;
            InkColor = ColorPalette.Colors[color].Hex;
        }
    }

    public void SetSizeOfBrush(string size)
    {
        SizeOfBrush = double.Parse(size);
        ResetSettings();
    }

    public void SetSizeOfBrushWithHotKey(BrushSize size) => SizeOfBrush = (double)size;

    private void ResetSettings()
    {
        _buttonSelectedStateHandler.ResetPreviousButton();
        _buttonSelectedStateHandler.ResetArrowButtonSelected();
        _settingPanelStateHandler.HideAllPanels();
    }
}
