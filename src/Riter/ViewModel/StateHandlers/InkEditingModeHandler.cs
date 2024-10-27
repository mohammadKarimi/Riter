using System.Windows.Controls;

namespace Riter.ViewModel.StateHandlers;
public class InkEditingModeStateHandler : BaseStateHandler, IInkEditingModeStateHandler
{
    private InkCanvasEditingMode _inkEditingMode = InkCanvasEditingMode.None;

    public InkCanvasEditingMode InkEditingMode
    {
        get => _inkEditingMode;
        private set => SetProperty(ref _inkEditingMode, value, nameof(InkEditingMode));
    }

    public void EraseByStroke() => InkEditingMode = InkCanvasEditingMode.EraseByStroke;

    public void Ink() => InkEditingMode = InkCanvasEditingMode.Ink;

    public void None() => InkEditingMode = InkCanvasEditingMode.None;

    public void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing) => InkEditingMode = inkCanvasEditing;
}
