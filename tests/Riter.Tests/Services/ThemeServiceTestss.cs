using Riter.Core;
using Riter.Core.Consts;
using Riter.Core.Enum;
using Riter.Services;
using Xunit;

namespace Riter.Tests.Services;
public class ThemeServiceTests
{
    private readonly AppSettings _mockAppSettings;
    private readonly ThemeService _themeService;

    public ThemeServiceTests()
    {
        _mockAppSettings = new AppSettings();
        _themeService = new ThemeService(_mockAppSettings);
    }

    [Fact]
    public void GetThemeColor_ShouldReturnCorrectColor_ForLightTheme()
    {
        var color = _themeService.GetThemeColor(ThemeColorKeys.CurrentTheme);
        color.Should().Be("#FFF");
    }

    [Fact]
    public void GetThemeColor_ShouldReturnCorrectColor_ForDarkTheme()
    {
        _mockAppSettings.Theme = Theme.Dark;
        _themeService.ToggleTheme();
        var color = _themeService.GetThemeColor(ThemeColorKeys.CurrentTheme);
        color.Should().Be("#2c2c2c");
    }

    [Fact]
    public void IsDarkMode_ShouldReturnFalse_WhenThemeIsLight()
    {
        _themeService.IsDarkMode().Should().BeFalse();
    }

    [Fact]
    public void IsDarkMode_ShouldReturnTrue_WhenThemeIsDark()
    {
        _themeService.ToggleTheme();
        _themeService.IsDarkMode().Should().BeTrue();
    }

    [Fact]
    public void ToggleTheme_ShouldSwitchBetweenLightAndDark()
    {
        _themeService.ToggleTheme();
        var firstToggle = _themeService.IsDarkMode();

        _themeService.ToggleTheme();
        var secondToggle = _themeService.IsDarkMode();
        firstToggle.Should().BeTrue();
        secondToggle.Should().BeFalse();
    }

    [Fact]
    public void GetCurrentThemeName_ShouldReturnCorrectTheme()
    {
        _themeService.GetCurrentThemeName().Should().Be($"Turn to {Theme.Dark}");

        _themeService.ToggleTheme();
        _themeService.GetCurrentThemeName().Should().Be($"Turn to {Theme.Light}");
    }
}
