using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riter.Core.Interfaces;
public interface IThemeService
{
    // ToolBox
    public void ToggleTheme();

    public string GetCurrentThemeIconsPath();

    public string GetCurrentThemeName();

    public string GetCurrentThemeHex();

    public string GetCurrentThemeTextsHex();

    public string GetCurrentThemeBorderHex();

    public string GetCurrentThemeIconsStrokeHex();

    public string GetCurrentThemeBlueButtonStrokeHex();

    public string GetCurrentThemeHoverBorderHex();

    public string GetCurrentThemeSelectedHex();

    public string GetCurrentThemeArrowButtonHoverHex();

    public bool IsDarkMode();

    // WindowControl
    public string GetWindowControlMouseHoverHex();

    // SettingPanel
    public string GetSettingButtonMouseOverHex();

    public string GetSettingButtonSelectedHex();
}
