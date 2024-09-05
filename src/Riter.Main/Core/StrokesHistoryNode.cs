using System.Windows.Ink;

namespace Riter.Main.Core;
public class StrokesHistoryNode(StrokeCollection strokes, StrokesHistoryNodeType type)
{
    public StrokeCollection Strokes { get; private set; } = strokes;
    public StrokesHistoryNodeType Type { get; private set; } = type;
}
