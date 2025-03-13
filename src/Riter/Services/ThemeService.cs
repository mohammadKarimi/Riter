using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.Core.IO;

namespace Riter.Services;
public class ThemeService(AppSettings appSettings) : IThemeService
{
    private readonly AppSettings _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    private Theme _currentTheme = appSettings.Theme;

    public string GetCurrentThemeHex() => _currentTheme == Theme.Light ? AppSettings.LightTheme : AppSettings.DarkTheme;

    public string GetCurrentThemeIconsPath() => _currentTheme == Theme.Light ? "Resources/DarkIcons.xaml" : "Resources/LightIcons.xaml";

    public string GetCurrentThemeBorderHex() => _currentTheme == Theme.Light ? "#1E1E1E" : "#FFF";

    public string GetCurrentThemeTextsHex() => _currentTheme == Theme.Light ? "#121212" : "#e6e6e6";

    public string GetCurrentThemeHoverBorderHex() => _currentTheme == Theme.Light ? "#317BF4" : "#7d7d7d";

    public string GetCurrentThemeIconsStrokeHex() => _currentTheme == Theme.Light ? "#1C1C1C" : "#E3E3E3";

    public string GetCurrentThemeBlueButtonStrokeHex() => _currentTheme == Theme.Light ? "#317BF4" : "#757575";

    public string GetCurrentThemeSelectedHex() => _currentTheme == Theme.Light ? "#FFF" : "#7d7d7d";

    public string GetCurrentThemeArrowButtonHoverHex() => _currentTheme == Theme.Light ? "#E5E5E5" : "#4f4e4e";

    public string GetCurrentThemeName() => _currentTheme.ToString();

    public bool IsDarkMode() => _currentTheme == Theme.Dark ? true : false;

    // WindowContols
    public string GetWindowControlMouseHoverHex() => _currentTheme == Theme.Light ? "#EEE" : "#7d7d7d";

    // SettingPanel
    public string GetSettingButtonMouseOverHex() => _currentTheme == Theme.Light ? "#F1F1F1 " : "#4f4e4e";

    public string GetSettingButtonSelectedHex() => _currentTheme == Theme.Light ? "#EEEEEE " : "#7d7d7d";

    public void ToggleTheme()
    {
        if(_currentTheme == Theme.Light)
        {
            _currentTheme = Theme.Dark;
        }
        else
        {
            _currentTheme = Theme.Light;
        }

        appSettings.Theme = _currentTheme;

        Task.Run(async () =>
        {
            await FileStorage.SaveConfig(_appSettings).ConfigureAwait(false);
        });
    }
}
