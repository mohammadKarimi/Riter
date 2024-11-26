using Moq;
using Riter.Core.Consts;
using Riter.Core.Enum;
using Riter.ViewModel.Handlers;
using Riter.ViewModel.StateHandlers;

namespace Riter.Tests.ViewModel.StateHandlers;
public class DrawingStateHandlerTests
{
    private readonly Mock<IInkEditingModeStateHandler> _inkEditingModeStateHandlerMock;
    private readonly Mock<IHighlighterStateHandler> _highlighterStateHandlerMock;
    private readonly Mock<IButtonSelectedStateHandler> _buttonSelectedStateHandlerMock;
    private readonly Mock<ISettingPanelStateHandler> _settingPanelStateHandlerMock;
    private readonly DrawingStateHandler _stateHandler;

    public DrawingStateHandlerTests()
    {
        _inkEditingModeStateHandlerMock = new Mock<IInkEditingModeStateHandler>();
        _highlighterStateHandlerMock = new Mock<IHighlighterStateHandler>();
        _buttonSelectedStateHandlerMock = new Mock<IButtonSelectedStateHandler>();
        _settingPanelStateHandlerMock = new Mock<ISettingPanelStateHandler>();

        _stateHandler = new DrawingStateHandler(
            _inkEditingModeStateHandlerMock.Object,
            _highlighterStateHandlerMock.Object,
            _buttonSelectedStateHandlerMock.Object,
            _settingPanelStateHandlerMock.Object);
    }

    [Fact]
    public void Should_SetReleasedState_When_ReleaseIsCalled()
    {
        _stateHandler.Release();

        _stateHandler.IsReleased.Should().BeTrue();
        _buttonSelectedStateHandlerMock.Verify(m => m.SetButtonSelectedName(ButtonNames.ReleaseButton), Times.Once);
        _inkEditingModeStateHandlerMock.Verify(m => m.None(), Times.Never);
    }

    [Fact]
    public void Should_StartDrawing_When_StartDrawingIsCalled()
    {
        _stateHandler.StartDrawing();

        _stateHandler.CurrentShape.Should().Be(DrawingShape.Line);
        _stateHandler.IsReleased.Should().BeFalse();
        _inkEditingModeStateHandlerMock.Verify(m => m.Ink(), Times.Exactly(2));
        _highlighterStateHandlerMock.Verify(m => m.DisableHighlighter(), Times.Once);
        _buttonSelectedStateHandlerMock.Verify(m => m.SetButtonSelectedName(ButtonNames.DrawingButton), Times.Once);
    }

    [Fact]
    public void Should_SetShape_When_StartDrawingShapeIsCalled()
    {
        _stateHandler.StartDrawingShape(DrawingShape.Circle);

        _stateHandler.IsReleased.Should().BeFalse();
        _highlighterStateHandlerMock.Verify(m => m.DisableHighlighter(), Times.Once);
        _buttonSelectedStateHandlerMock.Verify(m => m.SetButtonSelectedName(ButtonNames.ShapeDrawButton), Times.Once);
        _inkEditingModeStateHandlerMock.Verify(m => m.None(), Times.Once);
    }

    [Fact]
    public void Should_StartErasing_When_StartErasingIsCalled()
    {
        _stateHandler.StartErasing();

        _stateHandler.IsReleased.Should().BeFalse();
        _inkEditingModeStateHandlerMock.Verify(m => m.EraseByStroke(), Times.Once);
        _buttonSelectedStateHandlerMock.Verify(m => m.SetButtonSelectedName(ButtonNames.ErasingButton), Times.Once);
    }

    [Fact]
    public void Should_StartDrawing_When_StartErasingIsCalledTwice()
    {
        _buttonSelectedStateHandlerMock
            .Setup(m => m.ButtonSelectedName)
            .Returns(ButtonNames.ErasingButton);

        _stateHandler.StartErasing();

        _stateHandler.IsReleased.Should().BeFalse();
        _buttonSelectedStateHandlerMock.Verify(m => m.SetButtonSelectedName(ButtonNames.DrawingButton), Times.Once);

        _inkEditingModeStateHandlerMock.Verify(x => x.Ink(), Times.Exactly(2));
        _highlighterStateHandlerMock.Verify(m => m.DisableHighlighter(), Times.Once);
    }

    [Fact]
    public void Should_StartHighlighterDrawing_WhenStartHighlighterDrawingIsCalled()
    {
        _stateHandler.StartHighlighterDrawing();

        _stateHandler.CurrentShape.Should().Be(DrawingShape.Line);
        _stateHandler.IsReleased.Should().BeFalse();
        _highlighterStateHandlerMock.Verify(m => m.EnableHighlighter(), Times.Once);
        _inkEditingModeStateHandlerMock.Verify(m => m.Ink(), Times.Exactly(2));
        _buttonSelectedStateHandlerMock.Verify(m => m.SetButtonSelectedName(ButtonNames.HighlighterButton), Times.Once);
    }
}
