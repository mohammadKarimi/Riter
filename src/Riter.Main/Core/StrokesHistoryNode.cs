using System.Windows.Ink;
using Riter.Main.Core.Enum;

namespace Riter.Main.Core;

/// <summary>
/// This objects holds the stroke that user is drawind on the canvas.
/// </summary>
/// <param name="strokes">stroke line.</param>
/// <param name="type">type of drawing added or removed.</param>
public class StrokesHistoryNode
{
    private StrokesHistoryNode() { }

    /// <summary>
    /// Gets strokes of Drawing in canvas.
    /// </summary>
    public StrokeCollection Strokes { get; private set; }

    /// <summary>
    /// Gets type of Stroke Action which is Added or Removed.
    /// </summary>
    public StrokesHistoryNodeType Type { get; private set; }

    /// <summary>
    /// This method is a factory to create a new instance.
    /// </summary>
    /// <param name="strokes">the collection of Strokes.</param>
    /// <returns>New Instance of HistoryNode.</returns>
    public static StrokesHistoryNode CreateAddedType(StrokeCollection strokes) => new()
    {
        Strokes = strokes,
        Type = StrokesHistoryNodeType.Added,
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
