using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Riter.ViewModel.StateHandlers;

public interface IInkEditingModeStateHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    InkCanvasEditingMode InkEditingMode { get; }

    Brush Background { get; }

    void EnableBlackboard();
    void EnableTransparent();
    void EnableWhiteboard();
    void EraseByStroke();

    void Ink();

    void None();

    void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing);
}
