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

    public string CurrentThemeIconsPath
    {
        get
        {
            return _themeService.GetCurrentThemeIconsPath();
        }
    }

    public string CurrentThemeName
    {
        get
        {
            return _themeService.GetCurrentThemeName();
        }
    }

    public string CurrentThemeHex
    {
        get
        {
            return _themeService.GetCurrentThemeHex();
        }
    }

    public string CurrentThemeBorderHex
    {
        get
        {
            return _themeService.GetCurrentThemeBorderHex();
        }
    }

    public string CurrentThemeTextsHex
    {
        get
        {
            return _themeService.GetCurrentThemeTextsHex();
        }
    }

    // ToolBox & subPanels
    public string CurrentThemeIconsStrokeHex
    {
        get
        {
            return _themeService.GetCurrentThemeIconsStrokeHex();
        }
    }

    public string CurrentThemeBlueButtonStrokeHex
    {
        get
        {
            return _themeService.GetCurrentThemeBlueButtonStrokeHex();
        }
    }

    public string CurrentThemeHoverBorderHex
    {
        get
        {
            return _themeService.GetCurrentThemeHoverBorderHex();
        }
    }

    public string CurrentThemeSelectedHex
    {
        get
        {
            return _themeService.GetCurrentThemeSelectedHex();
        }
    }

    public string CurrentThemeArrowButtonHoverHex
    {
        get
        {
            return _themeService.GetCurrentThemeArrowButtonHoverHex();
        }
    }

    // WindowControl
    public string CurrentThemeWindowControlMouseHoverHex
    {
        get
        {
            return _themeService.GetWindowControlMouseHoverHex();
        }
    }

    // SettingPanel
    public string CurrentThemeSettingMouseOver
    {
        get
        {
            return _themeService.GetSettingButtonMouseOverHex();
        }
    }

    public string CurrentThemeSettingSelected
    {
        get
        {
            return _themeService.GetSettingButtonSelectedHex();
        }
    }

    public ICommand ToggleCommand => new RelayCommand(() =>
    {
        _themeService.ToggleTheme();
        ApplyTheme();
    });

    public void ApplyTheme()
    {
        OnStateChanged(this, new PropertyChangedEventArgs(nameof(IsDarkMode)));
        OnPropertyChanged(nameof(CurrentThemeIconsPath));
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
