namespace Riter.ViewModel;
public sealed class StrokeVisibilityViewModel : BaseViewModel
{
    private readonly IStrokeVisibilityStateHandler _strokeVisibilityHandler;
    private bool _isEnableFadeInk = false;

    public StrokeVisibilityViewModel(IStrokeVisibilityStateHandler strokeVisibilityHandler)
    {
        _strokeVisibilityHandler = strokeVisibilityHandler;
        _strokeVisibilityHandler.PropertyChanged += OnStateChanged;
    }

    public bool IsEnableFadeInk
    {
        get => _isEnableFadeInk;
        set
        {
            if (_isEnableFadeInk != value)
            {
                _isEnableFadeInk = value;
                OnPropertyChanged(nameof(IsEnableFadeInk));
            }
        }
    }

    public bool IsHideAll => _strokeVisibilityHandler.IsHideAll;

    public ICommand EnableFadeInk => new RelayCommand(EnableOrDisableFadeInk);

    public ICommand HideAllCommand => new RelayCommand(_strokeVisibilityHandler.HideAll);

    private void EnableOrDisableFadeInk() => IsEnableFadeInk = !IsEnableFadeInk;
}
