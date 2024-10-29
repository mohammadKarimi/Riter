using System.Windows.Controls;
using System.Windows.Media;

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

    public Brush Background => _inkEditingModeStateHandler.Background;

    public ICommand EnableBlackboardCommand => new RelayCommand(_inkEditingModeStateHandler.EnableBlackboard);

    public ICommand EnableTransparentCommand => new RelayCommand(_inkEditingModeStateHandler.EnableTransparent);

    public ICommand EnableWhiteboardCommand => new RelayCommand(_inkEditingModeStateHandler.EnableWhiteboard);
}
