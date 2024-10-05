using System.Windows.Controls;
using FluentAssertions;

namespace Riter.Tests.ViewModel;

public class PalleteStateTests
{
    private readonly PalleteState _state;
    private readonly TestState _testState;

    public PalleteStateTests()
    {
        _state = new PalleteState();
        _testState = new TestState();
    }

    [Fact]
    public void Should_UpdateStateToReleased_When_ReleasedButtonClicked()
    {
        _state.Release("ReleasedButton");

        _state.IsReleased.Should().BeTrue();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
        _state.ButtonSelectedName.Should().Be("ReleasedButton");
    }

    [Fact]
    public void Should_UpdateStateToDrawing_When_StartDrawing()
    {
        _state.StartDrawing("DrawingButton");
        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _state.ButtonSelectedName.Should().Be("DrawingButton");
    }

    [Fact]
    public void Should_UpdateStateToErasing_When_StartErasing()
    {
        _state.StartErasing("ErasingButton");

        _state.IsReleased.Should().BeFalse();
        _state.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
        _state.ButtonSelectedName.Should().Be("ErasingButton");
    }

    [Fact]
    public void Should_SetPropertyAndRaisePropertyChangedEvent_When_SetPropertyCalled()
    {
        var propertyName = string.Empty;
        _testState.PropertyChanged += (sender, e) => propertyName = e.PropertyName;
        _testState.TestProperty = "NewValue";
        _testState.TestProperty.Should().Be("NewValue");
        propertyName.Should().Be(nameof(_testState.TestProperty));
        _testState.OnChangedCalled.Should().BeTrue();
    }

    [Fact]
    public void Should_NotRaisePropertyChangedEvent_IfValueIsTheSame_When_SetPropertyCalled()
    {
        _testState.TestProperty = "InitialValue";
        var eventRaised = false;
        _testState.PropertyChanged += (sender, e) => eventRaised = true;
        _testState.TestProperty = "InitialValue";
        eventRaised.Should().BeFalse();
    }

    [Fact]
    public void Should_InvokeOnChangedAction_When_ValueChanges()
    {
        _testState.TestProperty = "ChangedValue";
        _testState.OnChangedCalled.Should().BeTrue();
    }

    private class TestState : PalleteState
    {
        public bool OnChangedCalled { get; private set; }

        private string _testProperty;
        public string TestProperty
        {
            get => _testProperty;
            set => SetProperty(ref _testProperty, value, nameof(TestProperty), () => OnChangedCalled = true);
        }
    }
}
