using Riter.ViewModel.StateHandlers;
using Xunit;

namespace Riter.Tests.ViewModel.StateHandlers;
public class StrokeVisibilityStateHandlerTests
{
    private readonly StrokeVisibilityStateHandler _handler;

    public StrokeVisibilityStateHandlerTests()
    {
        _handler = new StrokeVisibilityStateHandler();
    }

    [Fact]
    public void Should_Initialize_With_IsHideAll_False()
    {
        _handler.IsHideAll.Should().BeFalse();
    }

    [Fact]
    public void Should_Toggle_IsHideAll_When_HideAll_Is_Called()
    {
        _handler.HideAll();
        _handler.IsHideAll.Should().BeTrue(); 

        _handler.HideAll();
        _handler.IsHideAll.Should().BeFalse();
    }
}
