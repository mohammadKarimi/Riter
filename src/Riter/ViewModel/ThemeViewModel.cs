using Riter.Core.Consts;
using Riter.Core.Interfaces;

namespace Riter.ViewModel;
public class ThemeViewModel(IThemeService themeService) : BaseViewModel
{
    private readonly IThemeService _themeService = themeService;

    public bool IsDarkMode => _themeService.IsDarkMode();

    /// <summary>
    /// Current theme name in string (Dark, Light).
    /// </summary>
    public string CurrentThemeName => _themeService.GetCurrentThemeName();

    /// <summary>
    /// Current theme hexadecimal color in string.
    /// </summary>
    public string CurrentThemeHex => _themeService.GetThemeColor(ThemeColorKeys.CurrentTheme);

    /// <summary>
    /// Current theme panels border Color.
    /// </summary>
    public string CurrentThemeBorderHex => _themeService.GetThemeColor(ThemeColorKeys.Border);

    /// <summary>
    /// Current theme color of all texts in project.
    /// </summary>
    public string CurrentThemeTextsHex => _themeService.GetThemeColor(ThemeColorKeys.Text);

    /// <summary>
    /// Current theme color of some icons strokes in toolbox.
    /// </summary>
    public string CurrentThemeIconsStrokeHex => _themeService.GetThemeColor(ThemeColorKeys.IconsStroke);

    /// <summary>
    /// Current theme color of all subpanels items in project.
    /// </summary>
    public string CurrentThemeBlueButtonStrokeHex => _themeService.GetThemeColor(ThemeColorKeys.BlueButtonStroke);

    /// <summary>
    /// Current theme color of mousehover in toolbox.
    /// </summary>
    public string CurrentThemeHoverBorderHex => _themeService.GetThemeColor(ThemeColorKeys.HoverBorder);

    /// <summary>
    /// Current theme color of selected buttons in toolbox.
    /// </summary>
    public string CurrentThemeSelectedHex => _themeService.GetThemeColor(ThemeColorKeys.CurrentThemeSelected);

    /// <summary>
    /// Current theme color of arrow buttons in toolbox while hovering.
    /// </summary>
    public string CurrentThemeArrowButtonHoverHex => _themeService.GetThemeColor(ThemeColorKeys.ArrowButtonHover);

    /// <summary>
    /// Current theme color of buttons in WindowControl while hovering.
    /// </summary>
    public string CurrentThemeWindowControlMouseHoverHex => _themeService.GetThemeColor(ThemeColorKeys.WindowControlMouseHover);

    /// <summary>
    /// Current theme color of buttons in SettingPanel while hovering.
    /// </summary>
    public string CurrentThemeSettingMouseOver => _themeService.GetThemeColor(ThemeColorKeys.SettingButtonMouseOver);

    /// <summary>
    /// Current theme color of buttons in SettingPanel on selected mode.
    /// </summary>
    public string CurrentThemeSettingSelected => _themeService.GetThemeColor(ThemeColorKeys.SettingButtonSelected);

    /// <summary>
    /// Command of theme button in SettingPanel.
    /// </summary>
    public ICommand ToggleCommand => new RelayCommand(() =>
    {
        _themeService.ToggleTheme();
        ApplyTheme();
    });

    /// <summary>
    /// Created for applying theme.
    /// </summary>
    private void ApplyTheme()
    {
        OnPropertyChanged(nameof(IsDarkMode));
        OnPropertyChanged(nameof(CurrentThemeHex));
        OnPropertyChanged(nameof(CurrentThemeName));
        OnPropertyChanged(nameof(CurrentThemeTextsHex));
        OnPropertyChanged(nameof(CurrentThemeBorderHex));
        OnPropertyChanged(nameof(CurrentThemeSelectedHex));
        OnPropertyChanged(nameof(CurrentThemeHoverBorderHex));
        OnPropertyChanged(nameof(CurrentThemeIconsStrokeHex));
        OnPropertyChanged(nameof(CurrentThemeSettingSelected));
        OnPropertyChanged(nameof(CurrentThemeSettingMouseOver));
        OnPropertyChanged(nameof(CurrentThemeBlueButtonStrokeHex));
        OnPropertyChanged(nameof(CurrentThemeArrowButtonHoverHex));
        OnPropertyChanged(nameof(CurrentThemeWindowControlMouseHoverHex));
    }
}
