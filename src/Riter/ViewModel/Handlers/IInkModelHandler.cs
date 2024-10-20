using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Consts;

namespace Riter.ViewModel;


public interface IInkEditingModeHandler
{
    InkCanvasEditingMode InkEditingMode { get; }
    void SetInkCanvasEditingMode(InkCanvasEditingMode inkCanvasEditing);
}

public interface IButtonSelectionHandler
{
    string ButtonSelectedName { get; }
    void Release();
    void StartDrawing();
    void StartErasing();
    void EnableHighlighter();
}
