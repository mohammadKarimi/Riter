using System.Windows;
using System.Windows.Media;
using global::Riter.ViewModel.StateHandlers;

namespace Riter.Tests.ViewModel.StateHandlers;

public class InkEditingModeStateHandlerTests
{
    private readonly InkEditingModeStateHandler _stateHandler;

    static InkEditingModeStateHandlerTests()
    {
        if (Application.Current == null)
        {
            _ = new Application();
        }

        Application.Current.Resources["BlackBoard"] = Brushes.Black;
        Application.Current.Resources["Transparent"] = Brushes.Transparent;
        Application.Current.Resources["WhiteBoard"] = Brushes.White;
    }

    public InkEditingModeStateHandlerTests()
    {
        _stateHandler = new InkEditingModeStateHandler();
    }

    [Fact]
    public void Should_SetInkEditingModeToNone_When_NoneIsCalled()
    {
        _stateHandler.None();
        _stateHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
    }

    [Fact]
    public void Should_SetInkEditingModeToInk_When_InkIsCalled()
    {
        _stateHandler.Ink();
        _stateHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
    }

    [Fact]
    public void Should_SetInkEditingModeToEraseByStroke_When_EraseByStrokeIsCalled()
    {
        _stateHandler.EraseByStroke();
        _stateHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
    }

    [Fact]
    public void Should_SetBackgroundToBlackboard_When_EnableBlackboardIsCalled()
    {
        _stateHandler.EnableBlackboard();
        _stateHandler.Background.Should().Be(Brushes.Black);
    }

    [Fact]
    public void Should_SetBackgroundToTransparent_When_EnableTransparentIsCalled()
    {
        _stateHandler.EnableTransparent();
        _stateHandler.Background.Should().Be(Brushes.Transparent);
    }

    [Fact]
    public void Should_SetBackgroundToWhiteboard_When_EnableWhiteboardIsCalled()
    {
        _stateHandler.EnableWhiteboard();
        _stateHandler.Background.Should().Be(Brushes.White);
    }
}

