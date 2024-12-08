using Riter.Core.Interfaces;

namespace Riter.ViewModel;
public class InkCanvasViewModel(IStrokeHistoryService strokeHistoryService,
                                IInkEditingModeStateHandler inkEditingModeStateHandler,
                                IEnumerable<IShapeDrawer> shapeDrawers) : BaseViewModel
{
    public IStrokeHistoryService StrokeHistoryService { get; } = strokeHistoryService;

    public IInkEditingModeStateHandler InkEditingModeStateHandler { get; } = inkEditingModeStateHandler;

    public IEnumerable<IShapeDrawer> ShapeDrawer { get; } = shapeDrawers;
}
