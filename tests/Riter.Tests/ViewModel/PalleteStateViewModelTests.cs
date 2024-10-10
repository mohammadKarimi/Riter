using Riter.Core;
using Riter.Core.Interfaces;
using Riter.Services;

namespace Riter.Tests.ViewModel;

public class PalleteStateViewModelTests
{
    private readonly PalleteStateViewModel _viewModel;
    private readonly PalleteState _state;
    private readonly IStrokeHistoryService _strokeHistoryService;


    public PalleteStateViewModelTests()
    {
        _state = new PalleteState();
        _strokeHistoryService = new StrokeHistoryService();
        _viewModel = new PalleteStateViewModel(_state, _strokeHistoryService);
    }

    [Fact]
    public void Should_SetStateToReleased_When_ReleasedButtonClicked()
    {
        _viewModel.ReleasedButtonCommand.Execute("ReleasedButton");
        _state.IsReleased.Should().BeTrue();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
        _state.ButtonSelectedName.Should().Be("ReleasedButton");
    }

    [Fact]
    public void Should_SetStateToDrawing_When_DrawingButtonCommandClicked()
    {
        _viewModel.DrawingButtonCommand.Execute("DrawingButton");
        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _state.ButtonSelectedName.Should().Be("DrawingButton");
    }

    [Fact]
    public void ShouldToggled_When_DrawingButtonCommandDoubleClicked()
    {
        _viewModel.DrawingButtonCommand.Execute("DrawingButton");
        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _state.ButtonSelectedName.Should().Be("DrawingButton");

        _viewModel.DrawingButtonCommand.Execute("DrawingButton");
        _state.IsReleased.Should().BeTrue();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
        _state.ButtonSelectedName.Should().Be("ReleasedButton");
    }

    [Fact]
    public void Should_SetStateToErasing_When_ErasingButtonCommand()
    {
        _viewModel.ErasingButtonCommand.Execute("ErasingButton");
        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
        _state.ButtonSelectedName.Should().Be("ErasingButton");
    }
}
