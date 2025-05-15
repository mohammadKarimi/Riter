using Riter.ViewModel.Handlers;
using Xunit;

namespace Riter.Tests.ViewModel.StateHandlers;
public class HighlighterStateHandlerTests
{
    private readonly HighlighterStateHandler _stateHandler;

    public HighlighterStateHandlerTests()
    {
        _stateHandler = new HighlighterStateHandler();
    }

    [Fact]
    public void Should_DisableHighlighter_When_DisableHighlighterIsCalled()
    {
        _stateHandler.EnableHighlighter(); // Ensure the state is initially true.
        _stateHandler.DisableHighlighter();

        _stateHandler.IsHighlighter.Should().BeFalse();
    }

    [Fact]
    public void Should_EnableHighlighter_When_EnableHighlighterIsCalled()
    {
        _stateHandler.DisableHighlighter(); // Ensure the state is initially false.
        _stateHandler.EnableHighlighter();
        _stateHandler.IsHighlighter.Should().BeTrue();
    }
}
