using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Consts;

namespace Riter.ViewModel;

public interface IInkEditingModeHandler
{
    InkCanvasEditingMode InkEditingMode { get; }

    void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing);
}
