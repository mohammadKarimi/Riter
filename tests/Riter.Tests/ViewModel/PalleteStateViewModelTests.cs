namespace Riter.Tests.ViewModel;

public class PalleteStateViewModelTests
{
    private readonly PalleteStateViewModel _viewModel;
    private readonly PalleteState _state;

    public PalleteStateViewModelTests()
    {
        _state = new PalleteState();
        _viewModel = new PalleteStateViewModel(_state);
    }

    [Fact]
    public void ReleasedButtonCommand_ShouldSetStateToReleased()
    {
        _viewModel.ReleasedButtonCommand.Execute("ReleasedButton");
        _state.IsReleased.Should().BeTrue();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
        _state.ButtonSelectedName.Should().Be("ReleasedButton");
    }

    [Fact]
    public void DrawingButtonCommand_ShouldSetStateToDrawing()
    {
        _viewModel.DrawingButtonCommand.Execute("DrawingButton");
        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _state.ButtonSelectedName.Should().Be("DrawingButton");
    }

    [Fact]
    public void ErasingButtonCommand_ShouldSetStateToErasing()
    {
        _viewModel.ErasingButtonCommand.Execute("ErasingButton");
        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
        _state.ButtonSelectedName.Should().Be("ErasingButton");
    }
}
