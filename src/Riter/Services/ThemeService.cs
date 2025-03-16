using Riter.Core.Consts;
using Riter.Core.Enum;
using Riter.Core.Interfaces;
using Riter.Core.IO;

namespace Riter.Services;

public class ThemeService(AppSettings appSettings) : IThemeService
{
    private static readonly Dictionary<Theme, Dictionary<string, string>> ThemeColors = new()
    {
        [Theme.Light] = new()
        {
            [ThemeColorKeys.CurrentTheme] = "#FFF",
            [ThemeColorKeys.Border] = "#1E1E1E",
            [ThemeColorKeys.Text] = "#121212",
            [ThemeColorKeys.HoverBorder] = "#317BF4",
            [ThemeColorKeys.IconsStroke] = "#1C1C1C",
            [ThemeColorKeys.BlueButtonStroke] = "#317BF4",
            [ThemeColorKeys.CurrentThemeSelected] = "#FFF",
            [ThemeColorKeys.ArrowButtonHover] = "#E5E5E5",
            [ThemeColorKeys.WindowControlMouseHover] = "#EEE",
            [ThemeColorKeys.SettingButtonMouseOver] = "#F1F1F1",
            [ThemeColorKeys.SettingButtonSelected] = "#EEEEEE",
        },
        [Theme.Dark] = new()
        {
            [ThemeColorKeys.CurrentTheme] = "#2c2c2c",
            [ThemeColorKeys.Border] = "#FFF",
            [ThemeColorKeys.Text] = "#e6e6e6",
            [ThemeColorKeys.HoverBorder] = "#7d7d7d",
            [ThemeColorKeys.IconsStroke] = "#E3E3E3",
            [ThemeColorKeys.BlueButtonStroke] = "#757575",
            [ThemeColorKeys.CurrentThemeSelected] = "#7d7d7d",
            [ThemeColorKeys.ArrowButtonHover] = "#4f4e4e",
            [ThemeColorKeys.WindowControlMouseHover] = "#7d7d7d",
            [ThemeColorKeys.SettingButtonMouseOver] = "#4f4e4e",
            [ThemeColorKeys.SettingButtonSelected] = "#7d7d7d",
        },
    };

    private readonly AppSettings _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    private Theme _currentTheme = appSettings.Theme;

    public string GetThemeColor(string key) => ThemeColors[_currentTheme][key];

    public string GetCurrentThemeName() => _currentTheme.ToString();

    public bool IsDarkMode() => _currentTheme == Theme.Dark;

    public void ToggleTheme()
    {
        _currentTheme = _currentTheme == Theme.Light ? Theme.Dark : Theme.Light;
        _appSettings.Theme = _currentTheme;
        Task.Run(async () => await FileStorage.SaveConfig(_appSettings).ConfigureAwait(false));
    }
}
