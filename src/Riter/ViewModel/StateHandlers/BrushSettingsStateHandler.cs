using Riter.Core.Drawing;
using Riter.Core.Enum;
using EnumInkColor = Riter.Core.Enum.InkColor;

namespace Riter.ViewModel.StateHandlers;

public class BrushSettingsStateHandler : BaseStateHandler, IBrushSettingsStateHandler
{
    private readonly IButtonSelectedStateHandler _buttonSelectedStateHandler;
    private readonly ISettingPanelStateHandler _settingPanelStateHandler;
    private readonly AppSettings _appSettings;
    private string _inkColor;
    private string _colorSelected;
    private double _sizeOfBrush;
    private bool _isRainbow;

    public BrushSettingsStateHandler(
        IButtonSelectedStateHandler buttonSelectedStateHandler,
        ISettingPanelStateHandler settingPanelStateHandler,
        AppSettings appSettings)
    {
        _buttonSelectedStateHandler = buttonSelectedStateHandler;
        _settingPanelStateHandler = settingPanelStateHandler;

        InitializeDefaults();
        _appSettings = appSettings;
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
        UpdateRainbowMode(color);
        UpdateInkColor(color);
        ResetSettings();
    }

    public void SetInkColorWithHotKey(EnumInkColor color)
    {
        if (color == EnumInkColor.RainBow)
        {
            EnableRainbowMode(color.ToString());
        }
        else
        {
            DisableRainbowMode(ColorPalette.Colors[color].Hex);
        }
    }

    public void SetSizeOfBrush(string size)
    {
        if (double.TryParse(size, out var parsedSize))
        {
            SizeOfBrush = parsedSize;
            ResetSettings();
        }
        else
        {
            throw new ArgumentException("Invalid brush size format.", nameof(size));
        }
    }

    public void SetSizeOfBrushWithHotKey(BrushSize size) => SizeOfBrush = (double)size;

    private void InitializeDefaults()
    {
        SetInkColor(_appSettings.InkDefaultColor);
        SizeOfBrush = _appSettings.BrushSize;
    }

    private void UpdateRainbowMode(string color) => IsRainbow = color == EnumInkColor.RainBow.ToString();

    private void UpdateInkColor(string color)
    {
        InkColor = color;
        ColorSelected = color;
    }

    private void EnableRainbowMode(string color)
    {
        IsRainbow = true;
        InkColor = ColorSelected = color;
    }

    private void DisableRainbowMode(string colorHex)
    {
        IsRainbow = false;
        InkColor = colorHex;
    }

    private void ResetSettings()
    {
        _buttonSelectedStateHandler.ResetPreviousButton();
        _buttonSelectedStateHandler.ResetArrowButtonSelected();
        _settingPanelStateHandler.HideAllPanels();
    }
}
