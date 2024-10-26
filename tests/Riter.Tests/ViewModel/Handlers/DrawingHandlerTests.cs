using Riter.ViewModel.StateHandlers;

namespace Riter.Tests.ViewModel.Handlers;

public class DrawingHandlerTests
{
    private readonly DrawingStateHandler _handler;
    private readonly TestHanlder _testHandler;

    public DrawingHandlerTests()
    {
        _handler = new DrawingStateHandler();
        _testHandler = new TestHanlder();
    }

    [Fact]
    public void Should_UpdateStateToReleased_When_ReleasedButtonClicked()
    {
        _handler.Release();

        _handler.IsReleased.Should().BeTrue();
        _handler.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
    }

    [Fact]
    public void Should_UpdateStateToDrawing_When_StartDrawing()
    {
        _handler.StartDrawing();
        _handler.IsReleased.Should().BeFalse();
        _handler.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
        _handler.ButtonSelectedName.Should().Be("DrawingButton");
    }

    [Fact]
    public void Should_UpdateStateToErasing_When_StartErasing()
    {
        _handler.StartErasing();

        _handler.IsReleased.Should().BeFalse();
        _handler.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
    }

    [Fact]
    public void Should_SetPropertyAndRaisePropertyChangedEvent_When_SetPropertyCalled()
    {
        var propertyName = string.Empty;
        _testHandler.PropertyChanged += (sender, e) => propertyName = e.PropertyName;
        _testHandler.TestProperty = "NewValue";
        _testHandler.TestProperty.Should().Be("NewValue");
        propertyName.Should().Be(nameof(_testHandler.TestProperty));
        _testHandler.OnChangedCalled.Should().BeTrue();
    }

    [Fact]
    public void Should_NotRaisePropertyChangedEvent_IfValueIsTheSame_When_SetPropertyCalled()
    {
        _testHandler.TestProperty = "InitialValue";
        var eventRaised = false;
        _testHandler.PropertyChanged += (sender, e) => eventRaised = true;
        _testHandler.TestProperty = "InitialValue";
        eventRaised.Should().BeFalse();
    }

    [Fact]
    public void Should_InvokeOnChangedAction_When_ValueChanges()
    {
        _testHandler.TestProperty = "ChangedValue";
        _testHandler.OnChangedCalled.Should().BeTrue();
    }

    private class TestHanlder : DrawingStateHandler
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
