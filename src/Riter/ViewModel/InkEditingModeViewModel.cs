using System.Windows.Controls;

namespace Riter.ViewModel;
public class InkEditingModeViewModel : BaseViewModel
{
    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler;

    public InkEditingModeViewModel(IInkEditingModeStateHandler inkEditingModeStateHandler)
    {
        _inkEditingModeStateHandler = inkEditingModeStateHandler;
        _inkEditingModeStateHandler.PropertyChanged += OnStateChanged;
    }

    public InkCanvasEditingMode InkEditingMode => _inkEditingModeStateHandler.InkEditingMode;
}
