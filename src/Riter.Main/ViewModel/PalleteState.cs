using System.Windows.Controls;

namespace Riter.Main.ViewModel;

/// <summary>
/// This Object is aimed for holding the state of Drawing Stroke and Tools States.
/// </summary>
public class PalleteState
{
    public bool IsReleased { get; set; } = true;
    public InkCanvasEditingMode InkEditingMode { get; set; } = InkCanvasEditingMode.None;

    public string ButtonSelectedName { get; set; } = "ReleaseButton";
    public Stack<StrokesHistoryNode> DrawingHistory { get; set; } = [];
    public Stack<StrokesHistoryNode> RedoHistory { get; set; } = [];
}
