namespace Riter.ViewModel;
public class HighlighterViewModel : BaseViewModel
{
    private readonly IHighlighterStateHandler _highlighterStateHandler;

    public HighlighterViewModel(IHighlighterStateHandler highlighterStateHandler)
    {
        _highlighterStateHandler = highlighterStateHandler;
        _highlighterStateHandler.PropertyChanged += OnStateChanged;
    }

    public bool IsHighlighter => _highlighterStateHandler.IsHighlighter;
}
