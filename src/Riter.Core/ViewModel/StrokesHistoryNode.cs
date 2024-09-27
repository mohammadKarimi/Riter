using System.Windows.Ink;
using Riter.Main.Core.Enum;

namespace Riter.Main.ViewModel;
public class StrokesHistoryNode(StrokeCollection strokes, StrokesHistoryNodeType type)
{
    public StrokeCollection Strokes { get; private set; } = strokes;
    public StrokesHistoryNodeType Type { get; private set; } = type;
}
