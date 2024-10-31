using Riter.Core.Enum;

namespace Riter.ViewModel;
public class DrawingShapeViewModel : BaseViewModel
{
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

    public ICommand SetCircleShapeCommand => new RelayCommand(() => CurrentShape = DrawingShape.Circle);

    public ICommand SetSquareShapeCommand => new RelayCommand(() => CurrentShape = DrawingShape.Square);
}
