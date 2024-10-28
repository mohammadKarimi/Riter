namespace Riter.ViewModel.Handlers;
public class HighlighterStateHandler : BaseStateHandler, IHighlighterStateHandler
{
    private bool _isHighlighter;

    public bool IsHighlighter
    {
        get => _isHighlighter;
        private set => SetProperty(ref _isHighlighter, value, nameof(IsHighlighter));
    }

    public void DisableHighlighter() => IsHighlighter = false;

    public void EnableHighlighter() => IsHighlighter = true;
}
