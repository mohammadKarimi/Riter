namespace Riter.Main.Core;

public class PalleteState
{
    public bool IsReleased { get; set; }
    public Stack<StrokesHistoryNode> DrawingHistory { get; set; } = [];
    public Stack<StrokesHistoryNode> RedoHistory { get; set; } = [];
}
