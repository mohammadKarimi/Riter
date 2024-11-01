using System.Windows.Controls;
using Riter.Core.Enum;

namespace Riter.ViewModel;
public class DrawingShapeViewModel(IInkEditingModeStateHandler inkEditingModeStateHandler) : BaseViewModel
{
    private readonly IInkEditingModeStateHandler _inkEditingModeStateHandler = inkEditingModeStateHandler;
    private DrawingShape _currentShape = DrawingShape.FreeDraw;

    public DrawingShape CurrentShape
    {
        get => _currentShape;
        set
        {
            _currentShape = value;
            OnPropertyChanged(nameof(CurrentShape));
        }
    }

    public ICommand SetArrowShapeCommand => new RelayCommand(() => CurrentShape = DrawingShape.Arrow);

    public ICommand SetRectangleShapeCommand => new RelayCommand(() => CurrentShape = DrawingShape.Rectangle);

    public ICommand SetCircleShapeCommand => new RelayCommand(() =>
    {
        CurrentShape = DrawingShape.Circle;
        _inkEditingModeStateHandler.SetInkCanvasEditingMode(InkCanvasEditingMode.None);
    });
}
