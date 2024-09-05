namespace Riter.Main.Core;

/// <summary>
/// This Object is aimed for holding the state of Drawing Stroke and Tools States.
/// </summary>
public class PalleteState
{
    public bool IsReleased { get; set; }
    public Stack<StrokesHistoryNode> DrawingHistory { get; set; } = [];
    public Stack<StrokesHistoryNode> RedoHistory { get; set; } = [];
}
