using System.ComponentModel;

namespace Riter.ViewModel.StateHandlers;
public interface IHighlighterStateHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets a value indicating whether gets a value of Enabling Highlighter Pen.
    /// </summary>
    bool IsHighlighter { get; }

    void DisableHighlighter();

    void EnableHighlighter();
}
