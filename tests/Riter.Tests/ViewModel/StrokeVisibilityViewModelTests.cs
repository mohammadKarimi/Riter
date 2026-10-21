using Riter.ViewModel;
using Riter.ViewModel.StateHandlers;
using Moq;
using Xunit;

namespace Riter.Tests.ViewModel;

public class StrokeVisibilityViewModelTests
{
    private readonly Mock<IStrokeVisibilityStateHandler> _strokeVisibilityHandlerMock;
    private readonly Mock<ISettingPanelStateHandler> _settingPanelStateHandlerMock;
    private readonly StrokeVisibilityViewModel _viewModel;

    public StrokeVisibilityViewModelTests()
    {
        _strokeVisibilityHandlerMock = new Mock<IStrokeVisibilityStateHandler>();
        _settingPanelStateHandlerMock = new Mock<ISettingPanelStateHandler>();

        _viewModel = new StrokeVisibilityViewModel(
            _strokeVisibilityHandlerMock.Object,
            _settingPanelStateHandlerMock.Object);
    }

    [Fact]
    public void Should_HideAllStrokesAndPanels_When_HideAllCommandIsExecuted()
    {
        _viewModel.HideAllCommand.Execute(null);

        _strokeVisibilityHandlerMock.Verify(m => m.HideAll(), Times.Once);
        _settingPanelStateHandlerMock.Verify(m => m.HideAllPanels(), Times.Once);
    }
}
