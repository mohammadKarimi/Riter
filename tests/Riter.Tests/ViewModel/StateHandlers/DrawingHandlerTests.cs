using global::Riter.Core;
using global::Riter.Core.Enum;
using Moq;
using Riter.ViewModel.StateHandlers;

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
            _mockSettingPanelStateHandler.Object
        );
    }

    [Fact]
    public void Should_UpdateInkColorAndColorSelected_When_SetInkColorIsCalled()
    {

        var newColor = "Red";
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
        var color = InkColor.Red;
        _brushSettingsStateHandler.SetInkColorWithHotKey(color);
        _brushSettingsStateHandler.InkColor.Should().Be(ColorPalette.Colors[color].Hex);
    }

    [Fact]
    public void Should_UpdateSizeOfBrush_When_SetSizeOfBrushIsCalled()
    {
        var newSize = "5.0";
        _brushSettingsStateHandler.SetSizeOfBrush(newSize);

        _brushSettingsStateHandler.SizeOfBrush.Should().Be(5.0);
        _mockButtonSelectedStateHandler.Verify(m => m.ResetPreviousButton(), Times.Exactly(2));
        _mockButtonSelectedStateHandler.Verify(m => m.ResetArrowButtonSelected(), Times.Exactly(2));
        _mockSettingPanelStateHandler.Verify(m => m.HideAllPanels(), Times.Exactly(2));
    }

    [Fact]
    public void Should_UpdateSizeOfBrush_When_SetSizeOfBrushWithHotKeyIsCalled()
    {
        var newSize = BrushSize.X2;
        _brushSettingsStateHandler.SetSizeOfBrushWithHotKey(newSize);
        _brushSettingsStateHandler.SizeOfBrush.Should().Be((double)newSize);
    }
}
