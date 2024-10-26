using Riter.Core.Interfaces;
using Riter.Services;
using Riter.ViewModel.Handlers;

namespace Riter.Tests.ViewModel;

public class PalleteStateViewModelTests
{
    //private readonly PalleteStateOrchestratorViewModel _viewModel;
    //private readonly IDrawingHandler _DrawingHandler;
    //private readonly IStrokeHistoryService _strokeHistoryService;
    //private readonly IBrushSettingsHandler _brushSettingsHandler;
    //private readonly IStrokeVisibilityHandler _strokeVisibilityHandler;


    //public PalleteStateViewModelTests()
    //{
    //    _DrawingHandler = new DrawingHandler();
    //    _strokeHistoryService = new StrokeHistoryService();
    //    _brushSettingsHandler = new BrushSettingsHandler();
    //    _strokeVisibilityHandler = new StrokeVisibilityHandler();
    //    _viewModel = new PalleteStateOrchestratorViewModel(_DrawingHandler, _strokeHistoryService, _strokeVisibilityHandler, _brushSettingsHandler);
    //}

    //[Fact]
    //public void Should_SetStateToReleased_When_ReleasedButtonClicked()
    //{
    //    _viewModel.ReleasedButtonCommand.Execute("ReleasedButton");
    //    _DrawingHandler.IsReleased.Should().BeTrue();
    //    _DrawingHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
    //    _DrawingHandler.ButtonSelectedName.Should().Be("ReleasedButton");
    //}

    //[Fact]
    //public void Should_SetStateToDrawing_When_DrawingButtonCommandClicked()
    //{
    //    _viewModel.DrawingButtonCommand.Execute("DrawingButton");
    //    _DrawingHandler.IsReleased.Should().BeFalse();
    //    _DrawingHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
    //    _DrawingHandler.ButtonSelectedName.Should().Be("DrawingButton");
    //}

    //[Fact]
    //public void ShouldToggled_When_DrawingButtonCommandDoubleClicked()
    //{
    //    _viewModel.DrawingButtonCommand.Execute("DrawingButton");
    //    _DrawingHandler.IsReleased.Should().BeFalse();
    //    _DrawingHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.Ink);
    //    _DrawingHandler.ButtonSelectedName.Should().Be("DrawingButton");

    //    _viewModel.DrawingButtonCommand.Execute("DrawingButton");
    //    _DrawingHandler.IsReleased.Should().BeTrue();
    //    _DrawingHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.None);
    //}

    //[Fact]
    //public void Should_SetStateToErasing_When_ErasingButtonCommand()
    //{
    //    _viewModel.ErasingButtonCommand.Execute("ErasingButton");
    //    _DrawingHandler.IsReleased.Should().BeFalse();
    //    _DrawingHandler.InkEditingMode.Should().Be(InkCanvasEditingMode.EraseByStroke);
    //}
}
