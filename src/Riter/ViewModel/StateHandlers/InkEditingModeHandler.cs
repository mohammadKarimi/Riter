using System.Windows.Controls;
using System.Windows.Media;

namespace Riter.ViewModel.StateHandlers;
public class InkEditingModeStateHandler : BaseStateHandler, IInkEditingModeStateHandler
{
    private InkCanvasEditingMode _inkEditingMode = InkCanvasEditingMode.None;
    private Brush _background = Brushes.Transparent;

    public Brush Background
    {
        get => _background;
        set => SetProperty(ref _background, value, nameof(Background));
    }

    public InkCanvasEditingMode InkEditingMode
    {
        get => _inkEditingMode;
        private set => SetProperty(ref _inkEditingMode, value, nameof(InkEditingMode));
    }

    public void EnableBlackboard() => Background = Application.Current.Resources["BlackBoard"] as Brush;

    public void EnableTransparent() => Background = Application.Current.Resources["Transparent"] as Brush;

    public void EnableWhiteboard() => Background = Application.Current.Resources["WhiteBoard"] as Brush;

    public void EraseByStroke() => InkEditingMode = InkCanvasEditingMode.EraseByStroke;

    public void Ink() => InkEditingMode = InkCanvasEditingMode.Ink;

    public void None() => InkEditingMode = InkCanvasEditingMode.None;

}
