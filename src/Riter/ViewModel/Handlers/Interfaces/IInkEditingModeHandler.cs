using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Consts;

namespace Riter.ViewModel;

public interface IInkEditingModeHandler
{
    /// <summary>
    /// Gets the current ink editing mode for the InkCanvas.
    /// </summary>
    InkCanvasEditingMode InkEditingMode { get; }

    void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing);
}
