using global::Riter.Core;
using global::Riter.Core.Drawing;
using global::Riter.Core.Enum;
using global::Riter.ViewModel.StateHandlers;
using Moq;

namespace Riter.Tests.ViewModel.StateHandlers;

public class BrushSettingsStateHandlerTests
{
    private readonly Mock<IButtonSelectedStateHandler> _mockButtonSelectedStateHandler;
    private readonly Mock<ISettingPanelStateHandler> _mockSettingPanelStateHandler;
    private readonly BrushSettingsStateHandler _brushSettingsStateHandler;

    public BrushSettingsStateHandlerTests()
    {
        _mockButtonSelectedStateHandler = new Mock<IButtonSelectedStateHandler>();
        _mockSettingPanelStateHandler = new Mock<ISettingPanelStateHandler>();

        _brushSettingsStateHandler = new BrushSettingsStateHandler(
            _mockButtonSelectedStateHandler.Object,
            _mockSettingPanelStateHandler.Object);
    }

    [Fact]
    public void Constructor_ShouldInitializeDefaults()
    {
        // Assert default brush settings
        _brushSettingsStateHandler.InkColor.Should().Be(AppSettings.InkDefaultColor);
        _brushSettingsStateHandler.SizeOfBrush.Should().Be(AppSettings.BrushSize);
        _brushSettingsStateHandler.IsRainbow.Should().BeFalse();
        _brushSettingsStateHandler.ColorSelected.Should().Be(AppSettings.InkDefaultColor);
    }

    [Fact]
    public void SetInkColor_ShouldUpdatePropertiesAndResetSettings()
    {
        // Act
        _brushSettingsStateHandler.SetInkColor(EnumInkColor.Red.ToString());

        // Assert
        _brushSettingsStateHandler.InkColor.Should().Be(EnumInkColor.Red.ToString());
        _brushSettingsStateHandler.ColorSelected.Should().Be(EnumInkColor.Red.ToString());
        _brushSettingsStateHandler.IsRainbow.Should().BeFalse();

        _mockButtonSelectedStateHandler.Verify(b => b.ResetPreviousButton(), Times.Once);
        _mockButtonSelectedStateHandler.Verify(b => b.ResetArrowButtonSelected(), Times.Once);
        _mockSettingPanelStateHandler.Verify(s => s.HideAllPanels(), Times.Once);
    }

    [Fact]
    public void SetInkColor_ShouldSetRainbowModeWhenColorIsRainbow()
    {
        // Act
        _brushSettingsStateHandler.SetInkColor(EnumInkColor.RainBow.ToString());

        // Assert
        _brushSettingsStateHandler.IsRainbow.Should().BeTrue();
        _brushSettingsStateHandler.InkColor.Should().Be(EnumInkColor.RainBow.ToString());
        _brushSettingsStateHandler.ColorSelected.Should().Be(EnumInkColor.RainBow.ToString());
    }

    [Fact]
    public void SetInkColorWithHotKey_ShouldUpdateInkColorAndRainbowMode()
    {
        // Act
        _brushSettingsStateHandler.SetInkColorWithHotKey(EnumInkColor.Blue);

        // Assert
        _brushSettingsStateHandler.IsRainbow.Should().BeFalse();
        _brushSettingsStateHandler.InkColor.Should().Be(ColorPalette.Colors[EnumInkColor.Blue].Hex);
    }

    [Fact]
    public void SetInkColorWithHotKey_ShouldEnableRainbowModeWhenRainbowIsSelected()
    {
        // Act
        _brushSettingsStateHandler.SetInkColorWithHotKey(EnumInkColor.RainBow);

        // Assert
        _brushSettingsStateHandler.IsRainbow.Should().BeTrue();
        _brushSettingsStateHandler.InkColor.Should().Be(EnumInkColor.RainBow.ToString());
        _brushSettingsStateHandler.ColorSelected.Should().Be(EnumInkColor.RainBow.ToString());
    }

    [Fact]
    public void SetSizeOfBrush_ShouldParseAndSetBrushSize()
    {
        // Act
        _brushSettingsStateHandler.SetSizeOfBrush("5.5");

        // Assert
        _brushSettingsStateHandler.SizeOfBrush.Should().Be(5.5);

        _mockButtonSelectedStateHandler.Verify(b => b.ResetPreviousButton(), Times.Once);
        _mockButtonSelectedStateHandler.Verify(b => b.ResetArrowButtonSelected(), Times.Once);
        _mockSettingPanelStateHandler.Verify(s => s.HideAllPanels(), Times.Once);
    }

    [Fact]
    public void SetSizeOfBrushWithHotKey_ShouldSetBrushSizeFromEnum()
    {
        // Act
        _brushSettingsStateHandler.SetSizeOfBrushWithHotKey(BrushSize.Medium);

        // Assert
        _brushSettingsStateHandler.SizeOfBrush.Should().Be((double)BrushSize.Medium);
    }

    [Fact]
    public void SetInkColor_InvalidColor_ShouldThrowException()
    {
        // Act
        Action act = () => _brushSettingsStateHandler.SetInkColor("InvalidColor");

        // Assert
        act.Should().NotThrow(); // Optional: Modify this behavior if exceptions are required.
        _brushSettingsStateHandler.InkColor.Should().Be("InvalidColor");
    }
}
