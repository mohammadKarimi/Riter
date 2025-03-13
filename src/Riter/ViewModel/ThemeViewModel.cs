using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.Services;

namespace Riter.ViewModel;
public class ThemeViewModel(IThemeService themeService) : BaseViewModel
{
    private readonly IThemeService _themeService = themeService;

    public bool IsDarkMode
    {
        get
        {
            return _themeService.IsDarkMode();
        }
    }

    /// <summary>
    /// Current theme name in string (Dark, Light).
    /// </summary>
    public string CurrentThemeName
    {
        get
        {
            return _themeService.GetCurrentThemeName();
        }
    }

    /// <summary>
    /// Current theme hexadecimal color in string.
    /// </summary>
    public string CurrentThemeHex
    {
        get
        {
            return _themeService.GetCurrentThemeHex();
        }
    }

    /// <summary>
    /// Current theme panels border Color.
    /// </summary>
    public string CurrentThemeBorderHex
    {
        get
        {
            return _themeService.GetCurrentThemeBorderHex();
        }
    }

    /// <summary>
    /// Current theme color of all texts in project.
    /// </summary>
    public string CurrentThemeTextsHex
    {
        get
        {
            return _themeService.GetCurrentThemeTextsHex();
        }
    }

    /// <summary>
    /// Current theme color of some icons strokes in toolbox.
    /// </summary>
    public string CurrentThemeIconsStrokeHex
    {
        get
        {
            return _themeService.GetCurrentThemeIconsStrokeHex();
        }
    }

    /// <summary>
    /// Current theme color of all subpanels items in project.
    /// </summary>
    public string CurrentThemeBlueButtonStrokeHex
    {
        get
        {
            return _themeService.GetCurrentThemeBlueButtonStrokeHex();
        }
    }

    /// <summary>
    /// Current theme color of mousehover in toolbox.
    /// </summary>
    public string CurrentThemeHoverBorderHex
    {
        get
        {
            return _themeService.GetCurrentThemeHoverBorderHex();
        }
    }

    /// <summary>
    /// Current theme color of selected buttons in toolbox.
    /// </summary>
    public string CurrentThemeSelectedHex
    {
        get
        {
            return _themeService.GetCurrentThemeSelectedHex();
        }
    }

    /// <summary>
    /// Current theme color of arrow buttons in toolbox while hovering.
    /// </summary>
    public string CurrentThemeArrowButtonHoverHex
    {
        get
        {
            return _themeService.GetCurrentThemeArrowButtonHoverHex();
        }
    }

    /// <summary>
    /// Current theme color of buttons in WindowControl while hovering.
    /// </summary>
    public string CurrentThemeWindowControlMouseHoverHex
    {
        get
        {
            return _themeService.GetWindowControlMouseHoverHex();
        }
    }

    /// <summary>
    /// Current theme color of buttons in SettingPanel while hovering.
    /// </summary>
    public string CurrentThemeSettingMouseOver
    {
        get
        {
            return _themeService.GetSettingButtonMouseOverHex();
        }
    }

    /// <summary>
    /// Current theme color of buttons in SettingPanel on selected mode.
    /// </summary>
    public string CurrentThemeSettingSelected
    {
        get
        {
            return _themeService.GetSettingButtonSelectedHex();
        }
    }

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
        OnStateChanged(this, new PropertyChangedEventArgs(nameof(IsDarkMode)));
        OnPropertyChanged(nameof(CurrentThemeName));
        OnPropertyChanged(nameof(CurrentThemeHex));
        OnPropertyChanged(nameof(CurrentThemeBorderHex));
        OnPropertyChanged(nameof(CurrentThemeTextsHex));
        OnPropertyChanged(nameof(CurrentThemeIconsStrokeHex));
        OnPropertyChanged(nameof(CurrentThemeBlueButtonStrokeHex));
        OnPropertyChanged(nameof(CurrentThemeHoverBorderHex));
        OnPropertyChanged(nameof(CurrentThemeSelectedHex));
        OnPropertyChanged(nameof(CurrentThemeArrowButtonHoverHex));
        OnPropertyChanged(nameof(CurrentThemeWindowControlMouseHoverHex));
        OnPropertyChanged(nameof(CurrentThemeSettingMouseOver));
        OnPropertyChanged(nameof(CurrentThemeSettingSelected));
    }
}
