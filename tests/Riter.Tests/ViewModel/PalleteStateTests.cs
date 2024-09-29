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

    [Fact]
    public void SetProperty_ShouldSetPropertyAndRaisePropertyChangedEvent()
    {
        var propertyName = string.Empty;
        _testState.PropertyChanged += (sender, e) => propertyName = e.PropertyName;
        _testState.TestProperty = "NewValue";
        _testState.TestProperty.Should().Be("NewValue");
        propertyName.Should().Be(nameof(_testState.TestProperty));
        _testState.OnChangedCalled.Should().BeTrue();
    }

    [Fact]
    public void SetProperty_ShouldNotRaisePropertyChangedEvent_IfValueIsTheSame()
    {
        _testState.TestProperty = "InitialValue";
        var eventRaised = false;
        _testState.PropertyChanged += (sender, e) => eventRaised = true;
        _testState.TestProperty = "InitialValue";
        eventRaised.Should().BeFalse();
    }

    [Fact]
    public void SetProperty_ShouldInvokeOnChangedAction_IfValueChanges()
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
