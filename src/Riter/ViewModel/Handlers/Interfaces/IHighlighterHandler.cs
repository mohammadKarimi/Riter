namespace Riter.ViewModel.Handlers;
public interface IHighlighterHandler
{
    /// <summary>
    /// Gets a value indicating whether gets a value of Enabling Highlighter Pen.
    /// </summary>
    bool IsHighlighter { get; }

    void EnableHighlighter();
}
