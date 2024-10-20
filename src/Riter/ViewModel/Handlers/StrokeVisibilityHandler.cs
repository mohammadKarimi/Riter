namespace Riter.ViewModel.Handlers;

public class StrokeVisibilityHandler : BaseHandler, IStrokeVisibilityHandler
{
    private bool _isHideAll = false;

    /// <inheritdoc/>
    public bool IsHideAll
    {
        get => _isHideAll;
        private set => SetProperty(ref _isHideAll, value, nameof(IsHideAll));
    }

    /// <inheritdoc/>
    public void HideAll() => IsHideAll = !IsHideAll;
}
