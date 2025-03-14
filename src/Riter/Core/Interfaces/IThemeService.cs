using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riter.Core.Interfaces;
public interface IThemeService
{
    /// <summary>
    /// change current theme (between Dark & Light).
    /// </summary>
    public void ToggleTheme();

    /// <summary>
    /// name of the current theme.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeName();

    /// <summary>
    /// Hexadeciaml code of the current theme.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeHex();

    /// <summary>
    /// Hexadeciaml code of that used for texts in project.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeTextsHex();

    /// <summary>
    /// Hexadeciaml code of buttons border in panels.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeBorderHex();

    /// <summary>
    /// Hexadeciaml code of some icons strokes in toolbox.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeIconsStrokeHex();

    /// <summary>
    /// Hexadeciaml code of border and mouse hover color on subpanels items.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeBlueButtonStrokeHex();

    /// <summary>
    /// Current theme color of mousehover in toolbox.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeHoverBorderHex();

    /// <summary>
    /// Current theme color of selected buttons in toolbox.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeSelectedHex();

    /// <summary>
    /// Current theme color of arrow buttons in toolbox while hovering.
    /// </summary>
    /// <returns>string.</returns>
    public string GetCurrentThemeArrowButtonHoverHex();

    public bool IsDarkMode();

    /// <summary>
    /// Current theme color of buttons in WindowControl while hovering.
    /// </summary>
    /// <returns>string.</returns>
    public string GetWindowControlMouseHoverHex();

    /// <summary>
    /// Current theme color of buttons in SettingPanel while hovering.
    /// </summary>
    /// <returns>string.</returns>
    public string GetSettingButtonMouseOverHex();

    /// <summary>
    ///  Current theme color of buttons in SettingPanel on selected mode.
    /// </summary>
    /// <returns>string.</returns>
    public string GetSettingButtonSelectedHex();
}
