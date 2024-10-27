using System.ComponentModel;
using System.Windows.Controls;

namespace Riter.ViewModel.StateHandlers;

public interface IInkEditingModeStateHandler : INotifyPropertyChanged
{
    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    InkCanvasEditingMode InkEditingMode { get; }

    void EraseByStroke();

    void Ink();

    void None();

    void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing);
}
