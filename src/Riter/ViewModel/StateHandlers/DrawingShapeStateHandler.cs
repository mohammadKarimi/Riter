using Riter.Core.Enum;

namespace Riter.ViewModel.StateHandlers;
public class DrawingShapeStateHandler : BaseStateHandler, IDrawingShapeStateHandler
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

}
