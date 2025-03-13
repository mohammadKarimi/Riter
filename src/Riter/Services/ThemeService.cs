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

    public string GetCurrentThemeName() => _currentTheme.ToString();

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
