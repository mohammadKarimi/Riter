using System.Windows.Controls;
using FluentAssertions;

namespace Riter.Tests.ViewModel;

public class PalleteStateTests
{
    private readonly PalleteState _state;

    public PalleteStateTests()
    {
        _state = new PalleteState();
    }

    [Fact]
    public void Release_ShouldUpdateStateToReleased()
    {
        _state.Release("ReleasedButton");

        _state.IsReleased.Should().BeTrue();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
        _state.ButtonSelectedName.Should().Be("ReleasedButton");
    }

    [Fact]
    public void StartDrawing_ShouldUpdateStateToDrawing()
    {
        _state.StartDrawing("DrawingButton");
        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _state.ButtonSelectedName.Should().Be("DrawingButton");
    }

    [Fact]
    public void StartErasing_ShouldUpdateStateToErasing()
    {
        _state.StartErasing("ErasingButton");

        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
        _state.ButtonSelectedName.Should().Be("ErasingButton");
    }
}
