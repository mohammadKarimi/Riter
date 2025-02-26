using System.Windows.Ink;
using Riter.Core.Enum;

namespace Riter.Core.Drawing;

/// <summary>
/// This objects holds the stroke that user is drawing on the canvas.
/// </summary>
public class StrokesHistoryNode
{
    private StrokesHistoryNode()
    {
    }

    /// <summary>
    /// Gets strokes of Drawing in canvas.
    /// </summary>
    public StrokeCollection Strokes { get; private set; }

    /// <summary>
    /// Gets type of Stroke Action which is Added or Removed.
    /// </summary>
    public StrokesHistoryNodeType Type { get; private set; }

    public bool EnableTimer { get; private set; }

    public int TimerMilliSecond { get; private set; }

    public static StrokesHistoryNode CreateAddedType(StrokeCollection strokes, bool enableTimer, int timerMiliSecond) => new()
    {
        Strokes = strokes,
        Type = StrokesHistoryNodeType.Added,
        EnableTimer = enableTimer,
        TimerMilliSecond = timerMiliSecond,
    };

    /// <summary>
    /// This method is a factory to create a new instance.
    /// </summary>
    /// <param name="strokes">the collection of Strokes.</param>
    /// <returns>New Instance of HistoryNode.</returns>
    public static StrokesHistoryNode CreateRemovedType(StrokeCollection strokes) => new()
    {
        Strokes = strokes,
        Type = StrokesHistoryNodeType.Removed,
    };
}
