using Moq;
using Riter.Core.Consts;
using Riter.ViewModel.Handlers;
using Riter.ViewModel.StateHandlers;
using Xunit;

namespace Riter.Tests.ViewModel.StateHandlers;
public class SettingsPanelStateHandlerTests
{
    private readonly Mock<IButtonSelectedStateHandler> _mockButtonSelectedStateHandler;
    private readonly SettingsPanelStateHandler _handler;

    public SettingsPanelStateHandlerTests()
    {
        _mockButtonSelectedStateHandler = new Mock<IButtonSelectedStateHandler>();
        _handler = new SettingsPanelStateHandler(_mockButtonSelectedStateHandler.Object);
    }

    [Fact]
    public void Should_HideAllPanels_When_HideAllPanelsIsCalled()
    {

        _handler.HideAllPanels();

        _handler.SettingPanelVisibility.Should().BeFalse();
        _handler.BrushPanelVisibility.Should().BeFalse();
        _handler.ShapePanelVisibility.Should().BeFalse();
        _handler.ColorPanelVisibility.Should().BeFalse();
        _handler.SettingButtonClicked.Should().BeFalse();

        _mockButtonSelectedStateHandler.Verify(m => m.ResetArrowButtonSelected(), Times.Once);
    }

    [Fact]
    public void Should_SetSettingPanelVisibilityToTrue_When_SetSettingPanelVisibleIsCalled()
    {

        _handler.SetSettingPanelVisibile();
        _handler.SettingPanelVisibility.Should().BeTrue();
    }

    [Fact]
    public void Should_HideAllPanelsAndResetArrowButton_When_ToggleShapePanelCalled()
    {
        _mockButtonSelectedStateHandler.SetupGet(m => m.ArrowButtonSelectedName).Returns("BrushButton");

        _handler.ToggleShapePanel("BrushButton");

        _handler.BrushPanelVisibility.Should().BeFalse();
        _handler.SettingPanelVisibility.Should().BeFalse();
        _handler.ShapePanelVisibility.Should().BeTrue();
        _handler.SettingButtonClicked.Should().BeFalse();
        _handler.ColorPanelVisibility.Should().BeFalse();
        _mockButtonSelectedStateHandler.Verify(m => m.ResetArrowButtonSelected(), Times.Once);
    }

    [Fact]
    public void Should_HideAllPanelsAndResetArrowButton_When_ToggleBrushSettingsPanelIsCalledWithSameButton()
    {
        _mockButtonSelectedStateHandler.SetupGet(m => m.ArrowButtonSelectedName).Returns("BrushButton");

        _handler.ToggleBrushSettingsPanel("BrushButton");

        _handler.BrushPanelVisibility.Should().BeTrue();
        _handler.SettingPanelVisibility.Should().BeFalse();
        _handler.ShapePanelVisibility.Should().BeFalse();
        _handler.SettingButtonClicked.Should().BeFalse();
        _handler.ColorPanelVisibility.Should().BeFalse();
        _mockButtonSelectedStateHandler.Verify(m => m.ResetArrowButtonSelected(), Times.Once);
    }

    [Fact]
    public void Should_ShowBrushPanel_When_ToggleBrushSettingsPanelIsCalledWithDifferentButton()
    {
        _handler.ToggleBrushSettingsPanel("NewBrushButton");

        _handler.BrushPanelVisibility.Should().BeTrue();
        _mockButtonSelectedStateHandler.Verify(m => m.SetArrowButtonSelected(ButtonNames.ChangeBrushSettingButton), Times.Once);
    }
}
