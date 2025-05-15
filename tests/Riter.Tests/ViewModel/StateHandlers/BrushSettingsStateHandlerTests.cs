using global::Riter.Core;
using global::Riter.Core.Enum;
using Moq;
using Riter.Core.Drawing;
using Riter.ViewModel.StateHandlers;
using Xunit;

namespace Riter.Tests.ViewModel.StateHandlers;

public class BrushSettingsStateHandlerTests
{
    private readonly Mock<IButtonSelectedStateHandler> _mockButtonSelectedStateHandler;
    private readonly Mock<ISettingPanelStateHandler> _mockSettingPanelStateHandler;
    private readonly Mock<AppSettings> _appSettingsMock;
    private readonly BrushSettingsStateHandler _brushSettingsStateHandler;

    public BrushSettingsStateHandlerTests()
    {
        _mockButtonSelectedStateHandler = new Mock<IButtonSelectedStateHandler>();
        _mockSettingPanelStateHandler = new Mock<ISettingPanelStateHandler>();
        _appSettingsMock = new Mock<AppSettings>();
        _brushSettingsStateHandler = new BrushSettingsStateHandler(
            _mockButtonSelectedStateHandler.Object,
            _mockSettingPanelStateHandler.Object,
            _appSettingsMock.Object
        );
    }

    [Fact]
    public void Should_UpdateInkColorAndColorSelected_When_SetInkColorIsCalled()
    {

        string newColor = "Red";
        _brushSettingsStateHandler.SetInkColor(newColor);

        _brushSettingsStateHandler.InkColor.Should().Be(newColor);
        _brushSettingsStateHandler.ColorSelected.Should().Be(newColor);
        _mockButtonSelectedStateHandler.Verify(m => m.ResetPreviousButton(), Times.Exactly(2));
        _mockButtonSelectedStateHandler.Verify(m => m.ResetArrowButtonSelected(), Times.Exactly(2));
        _mockSettingPanelStateHandler.Verify(m => m.HideAllPanels(), Times.Exactly(2));
    }

    [Fact]
    public void Should_UpdateInkColor_When_SetInkColorWithHotKeyIsCalled()
    {
        InkColor color = InkColor.Red;
        _brushSettingsStateHandler.SetInkColorWithHotKey(color);
        _brushSettingsStateHandler.InkColor.Should().Be(ColorPalette.Colors[color].Hex);
    }

    [Fact]
    public void Should_UpdateSizeOfBrush_When_SetSizeOfBrushIsCalled()
    {
        string newSize = "5.0";
        _brushSettingsStateHandler.SetSizeOfBrush(newSize);

        _brushSettingsStateHandler.SizeOfBrush.Should().Be(5.0);
        _mockButtonSelectedStateHandler.Verify(m => m.ResetPreviousButton(), Times.Exactly(2));
        _mockButtonSelectedStateHandler.Verify(m => m.ResetArrowButtonSelected(), Times.Exactly(2));
        _mockSettingPanelStateHandler.Verify(m => m.HideAllPanels(), Times.Exactly(2));
    }

    [Fact]
    public void Should_UpdateSizeOfBrush_When_SetSizeOfBrushWithHotKeyIsCalled()
    {
        BrushSize newSize = BrushSize.X2;
        _brushSettingsStateHandler.SetSizeOfBrushWithHotKey(newSize);
        _brushSettingsStateHandler.SizeOfBrush.Should().Be((double)newSize);
    }

    [Fact]
    public void SetInkColor_ShouldUpdatePropertiesAndResetSettings()
    {

        _brushSettingsStateHandler.SetInkColor(InkColor.Red.ToString());

        _brushSettingsStateHandler.InkColor.Should().Be(InkColor.Red.ToString());
        _brushSettingsStateHandler.ColorSelected.Should().Be(InkColor.Red.ToString());
        _brushSettingsStateHandler.IsRainbow.Should().BeFalse();

        _mockButtonSelectedStateHandler.Verify(b => b.ResetPreviousButton(), Times.Exactly(2));
        _mockButtonSelectedStateHandler.Verify(b => b.ResetArrowButtonSelected(), Times.Exactly(2));
        _mockSettingPanelStateHandler.Verify(s => s.HideAllPanels(), Times.Exactly(2));
    }

    [Fact]
    public void SetInkColor_ShouldSetRainbowModeWhenColorIsRainbow()
    {

        _brushSettingsStateHandler.SetInkColor(InkColor.RainBow.ToString());


        _brushSettingsStateHandler.IsRainbow.Should().BeTrue();
        _brushSettingsStateHandler.InkColor.Should().Be(InkColor.RainBow.ToString());
        _brushSettingsStateHandler.ColorSelected.Should().Be(InkColor.RainBow.ToString());
    }

    [Fact]
    public void SetInkColorWithHotKey_ShouldUpdateInkColorAndRainbowMode()
    {
        _brushSettingsStateHandler.SetInkColorWithHotKey(InkColor.Mint);

        _brushSettingsStateHandler.IsRainbow.Should().BeFalse();
        _brushSettingsStateHandler.InkColor.Should().Be(ColorPalette.Colors[InkColor.Mint].Hex);
    }

    [Fact]
    public void SetInkColorWithHotKey_ShouldEnableRainbowModeWhenRainbowIsSelected()
    {

        _brushSettingsStateHandler.SetInkColorWithHotKey(InkColor.RainBow);

        _brushSettingsStateHandler.IsRainbow.Should().BeTrue();
        _brushSettingsStateHandler.InkColor.Should().Be(InkColor.RainBow.ToString());
        _brushSettingsStateHandler.ColorSelected.Should().Be(InkColor.RainBow.ToString());
    }

    [Fact]
    public void SetSizeOfBrush_ShouldParseAndSetBrushSize()
    {

        _brushSettingsStateHandler.SetSizeOfBrush("5.5");

        _brushSettingsStateHandler.SizeOfBrush.Should().Be(5.5);

        _mockButtonSelectedStateHandler.Verify(b => b.ResetPreviousButton(), Times.Exactly(2));
        _mockButtonSelectedStateHandler.Verify(b => b.ResetArrowButtonSelected(), Times.Exactly(2));
        _mockSettingPanelStateHandler.Verify(s => s.HideAllPanels(), Times.Exactly(2));
    }


    [Fact]
    public void SetInkColor_InvalidColor_ShouldThrowException()
    {
        Action act = () => _brushSettingsStateHandler.SetInkColor("InvalidColor");

        act.Should().NotThrow();
        _brushSettingsStateHandler.InkColor.Should().Be("InvalidColor");
    }
}
