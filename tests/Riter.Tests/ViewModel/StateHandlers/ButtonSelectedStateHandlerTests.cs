using Riter.ViewModel.StateHandlers;

namespace Riter.Tests.ViewModel.StateHandlers;
public class ButtonSelectedStateHandlerTests
{
    private readonly ButtonSelectedStateHandler _stateHandler;

    public ButtonSelectedStateHandlerTests()
    {
        _stateHandler = new ButtonSelectedStateHandler();
    }

    [Fact]
    public void Should_UpdateButtonSelectedName_When_SetButtonSelectedNameIsCalled()
    {
        var newButtonName = "NewButton";
        _stateHandler.SetButtonSelectedName(newButtonName);
        _stateHandler.ButtonSelectedName.Should().Be(newButtonName);
    }

    [Fact]
    public void Should_ResetButtonSelectedNameToPrevious_When_ResetPreviousButtonIsCalled()
    {
        var initialButtonName = "InitialButton";
        var newButtonName = "NewButton";
        _stateHandler.SetButtonSelectedName(initialButtonName);
        _stateHandler.StoreCurrentButton();
        _stateHandler.SetButtonSelectedName(newButtonName);

        _stateHandler.ResetPreviousButton();
        _stateHandler.ButtonSelectedName.Should().Be(initialButtonName);
    }

    [Fact]
    public void Should_ClearPreviousButtonSelectedName_When_ResetPreviousButtonIsCalledTwice()
    {
        var initialButtonName = "InitialButton";
        _stateHandler.SetButtonSelectedName(initialButtonName);
        _stateHandler.StoreCurrentButton();
        _stateHandler.ResetPreviousButton();
        _stateHandler.ResetPreviousButton();
        _stateHandler.ButtonSelectedName.Should().Be(initialButtonName);
    }

    [Fact]
    public void Should_StoreCurrentButtonSelectedName_When_StoreCurrentButtonIsCalled()
    {
        var initialButtonName = "InitialButton";
        _stateHandler.SetButtonSelectedName(initialButtonName);

        _stateHandler.StoreCurrentButton();
        _stateHandler.SetButtonSelectedName("NewButton");
        _stateHandler.ResetPreviousButton();
        _stateHandler.ButtonSelectedName.Should().Be(initialButtonName);
    }

    [Fact]
    public void Should_ResetArrowButtonSelectedName_When_ResetArrowButtonSelectedIsCalled()
    {
        var arrowButtonName = "ArrowButton";
        _stateHandler.SetArrowButtonSelected(arrowButtonName);

        _stateHandler.ResetArrowButtonSelected();
        _stateHandler.ArrowButtonSelectedName.Should().BeNullOrEmpty();
    }

    [Fact]
    public void Should_UpdateArrowButtonSelectedName_When_SetArrowButtonSelectedIsCalled()
    {
        var arrowButtonName = "ArrowButton";
        _stateHandler.SetArrowButtonSelected(arrowButtonName);
        _stateHandler.ArrowButtonSelectedName.Should().Be(arrowButtonName);
    }
}
