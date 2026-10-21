using Riter.Core;
using Riter.Services;
using Xunit;

namespace Riter.Tests.Services;
public class HotKeyCommandServiceTests
{
    private readonly AppSettings _mockAppSettings = new AppSettings
    {
        HotKeysConfig =
            [
                new HotKeysConfig { Key = "Arrow", Value = "CTRL + A" },
                new HotKeysConfig { Key = "Line", Value = "CTRL + L" },
                new HotKeysConfig { Key = "Release", Value = "R" }
            ]
    };

    private readonly HotKeyCommandService _service;
    private readonly Dictionary<HotKey, Action> _commands;

    public HotKeyCommandServiceTests()
    {
        _service = new HotKeyCommandService(_mockAppSettings);
        _commands = [];
    }

    [Fact]
    public void ExecuteHotKey_ShouldInvokeCorrectCommand()
    {
        bool wasCalled = false;
        _commands[HotKey.Arrow] = () => wasCalled = true;
        _service.InitializeCommands(_commands);
        HotKeiesPressed hotKeyPressed = new() { CtrlPressed = true, Key = "A" };

        _service.ExecuteHotKey(hotKeyPressed);

        wasCalled.Should().BeTrue();
    }

    [Fact]
    public void ExecuteHotKey_ShouldNotInvokeCommand_WhenHotKeyNotConfigured()
    {
        bool wasCalled = false;
        _commands[HotKey.Line] = () => wasCalled = true;
        _service.InitializeCommands(_commands);
        HotKeiesPressed hotKeyPressed = new() { CtrlPressed = true, Key = "X" };

        _service.ExecuteHotKey(hotKeyPressed);

        wasCalled.Should().BeFalse();
    }

    [Fact]
    public void IsHotKeyPressed_ShouldReturnTrue_WhenKeyMatchesConfiguredReleaseHotKey()
    {
        HotKeiesPressed hotKeyPressed = new() { Key = "R" };

        bool result = _service.IsHotKeyPressed(hotKeyPressed, HotKey.Release);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsHotKeyPressed_ShouldReturnFalse_WhenKeyDoesNotMatchConfiguredReleaseHotKey()
    {
        HotKeiesPressed hotKeyPressed = new() { Key = "D" };

        bool result = _service.IsHotKeyPressed(hotKeyPressed, HotKey.Release);

        result.Should().BeFalse();
    }
}
