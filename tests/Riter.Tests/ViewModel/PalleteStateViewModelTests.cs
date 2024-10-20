using Riter.Core.Interfaces;
using Riter.Services;
using Riter.ViewModel.Handlers;

namespace Riter.Tests.ViewModel;

public class PalleteStateViewModelTests
{
    private readonly PalleteStateViewModel _viewModel;
    private readonly IButtonSelectionHandler _buttonSelectionHandler;
    private readonly IStrokeHistoryService _strokeHistoryService;
    private readonly IBrushSettingsHandler _brushSettingsHandler;
    private readonly IStrokeVisibilityHandler _strokeVisibilityHandler;


    public PalleteStateViewModelTests()
    {
        _buttonSelectionHandler = new ButtonSelectionHandler();
        _strokeHistoryService = new StrokeHistoryService();
        _brushSettingsHandler = new BrushSettingsHandler();
        _strokeVisibilityHandler = new StrokeVisibilityHandler();
        _viewModel = new PalleteStateViewModel(_buttonSelectionHandler, _strokeHistoryService, _strokeVisibilityHandler, _brushSettingsHandler);
    }

    [Fact]
    public void Should_SetStateToReleased_When_ReleasedButtonClicked()
    {
        _viewModel.ReleasedButtonCommand.Execute("ReleasedButton");
        _buttonSelectionHandler.IsReleased.Should().BeTrue();
        _buttonSelectionHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
        _buttonSelectionHandler.ButtonSelectedName.Should().Be("ReleasedButton");
    }

    [Fact]
    public void Should_SetStateToDrawing_When_DrawingButtonCommandClicked()
    {
        _viewModel.DrawingButtonCommand.Execute("DrawingButton");
        _buttonSelectionHandler.IsReleased.Should().BeFalse();
        _buttonSelectionHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _buttonSelectionHandler.ButtonSelectedName.Should().Be("DrawingButton");
    }

    [Fact]
    public void ShouldToggled_When_DrawingButtonCommandDoubleClicked()
    {
        _viewModel.DrawingButtonCommand.Execute("DrawingButton");
        _buttonSelectionHandler.IsReleased.Should().BeFalse();
        _buttonSelectionHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _buttonSelectionHandler.ButtonSelectedName.Should().Be("DrawingButton");

        _viewModel.DrawingButtonCommand.Execute("DrawingButton");
        _buttonSelectionHandler.IsReleased.Should().BeTrue();
        _buttonSelectionHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
    }

    [Fact]
    public void Should_SetStateToErasing_When_ErasingButtonCommand()
    {
        _viewModel.ErasingButtonCommand.Execute("ErasingButton");
        _buttonSelectionHandler.IsReleased.Should().BeFalse();
        _buttonSelectionHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
        _buttonSelectionHandler.ButtonSelectedName.Should().Be("ErasingButton");
    }
}
