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

    public bool IsDarkMode();

    public string GetThemeColor(string key);
}
