namespace Riter.ViewModel.StateHandlers;

public class StrokeVisibilityStateHandler : BaseStateHandler, IStrokeVisibilityStateHandler
{
    private bool _isHideAll = false;

    public bool IsHideAll
    {
        get => _isHideAll;
        private set => SetProperty(ref _isHideAll, value, nameof(IsHideAll));
    }

    public void HideAll() => IsHideAll = !IsHideAll;
}
