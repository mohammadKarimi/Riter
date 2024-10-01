using System.Windows.Ink;
using Riter.Main.Core.Enum;

namespace Riter.Main.Core;

/// <summary>
/// This objects holds the stroke that user is drawind on the canvas.
/// </summary>
/// <param name="strokes">stroke line.</param>
/// <param name="type">type of drawing added or removed.</param>
public class StrokesHistoryNode(StrokeCollection strokes, StrokesHistoryNodeType type)
{
    /// <summary>
    /// Gets strokes of Drawing in canvas.
    /// </summary>
    public StrokeCollection Strokes { get; private set; } = strokes;

    /// <summary>
    /// Gets type of Stroke Action which is Added or Removed
    /// </summary>
    public StrokesHistoryNodeType Type { get; private set; } = type;
}
