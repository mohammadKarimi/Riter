namespace Riter.ViewModel.StateHandlers;
public interface IHighlighterStateHandler
{
    /// <summary>
    /// Gets a value indicating whether gets a value of Enabling Highlighter Pen.
    /// </summary>
    bool IsHighlighter { get; }

    void EnableHighlighter();
}
