namespace Riter.ViewModel;
public sealed class StrokeVisibilityViewModel : BaseViewModel
{
    private readonly IStrokeVisibilityStateHandler _strokeVisibilityHandler;

    public StrokeVisibilityViewModel(IStrokeVisibilityStateHandler strokeVisibilityHandler)
    {
        _strokeVisibilityHandler = strokeVisibilityHandler;
        _strokeVisibilityHandler.PropertyChanged += OnStateChanged;
    }

    public bool IsHideAll => _strokeVisibilityHandler.IsHideAll;

    public ICommand HideAllCommand => new RelayCommand(_strokeVisibilityHandler.HideAll);
}
