using System.Windows.Controls;
using System.Xml.Linq;
using Riter.Main.Core;
using Riter.Main.Core.Enum;
using Riter.Main.Core.Interfaces;

namespace Riter.Main.Services;

/// <inheritdoc/>
internal class StrokeHistoryService(InkCanvas inkCanvas) : IStrokeHistoryService
{
    private readonly Stack<StrokesHistoryNode> _history = [];
    private readonly Stack<StrokesHistoryNode> _redoHistory = [];

    private InkCanvas _inkCanvas { get; } = inkCanvas;

    /// <inheritdoc/>
    public bool CanRedo() => _redoHistory.Count != 0;

    /// <inheritdoc/>
    public bool CanUndo() => _history.Count != 0;

    /// <inheritdoc/>
    public void Clear()
    {
        _history.Clear();
        _redoHistory.Clear();
    }

    /// <inheritdoc/>
    public void ClearRedoHistory() => _redoHistory.Clear();

    /// <inheritdoc/>
    public StrokesHistoryNode Pop() => _history.Count == 0 ? null : _history.Pop();

    /// <inheritdoc/>
    public void Push(StrokesHistoryNode node) => _history.Push(node);

    /// <inheritdoc/>
    public void Redo()
    {
        if (!CanRedo())
        {
            return;
        }

        var lastItem = _redoHistory.Pop();
        if (lastItem.Type == StrokesHistoryNodeType.Removed)
        {
            _inkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            _inkCanvas.Strokes.Add(lastItem.Strokes);
        }

        Push(lastItem);
    }

    /// <inheritdoc/>
    public void Undo()
    {
        if (!CanUndo())
        {
            return;
        }

        var lastItem = Pop();
        if (lastItem.Type == StrokesHistoryNodeType.Added)
        {
            _inkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            _inkCanvas.Strokes.Add(lastItem.Strokes);
        }

        _redoHistory.Push(lastItem);
    }
}
