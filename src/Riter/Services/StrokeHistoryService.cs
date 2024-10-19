using System.Windows.Controls;
using Riter.Core;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Services;

/// <inheritdoc/>
public class StrokeHistoryService : IStrokeHistoryService
{
    private readonly Stack<StrokesHistoryNode> _history = [];
    private readonly Stack<StrokesHistoryNode> _redoHistory = [];
    private bool _ignoreStrokesChange;

    /// <summary>
    /// Gets or sets a value indicating whether return ignore strokeChnage backing field.
    /// </summary>
    public bool IgnoreStrokesChange
    {
        get => _ignoreStrokesChange;
        set => _ignoreStrokesChange = value;
    }

    private InkCanvas InkCanvas { get; set; }

    /// <inheritdoc/>
    public void SetMainElementToRedoAndUndo(InkCanvas canvas) => InkCanvas = canvas;

    /// <inheritdoc/>
    public bool CanRedo() => _redoHistory.Count != 0;

    /// <inheritdoc/>
    public bool CanUndo() => _history.Count != 0;

    /// <inheritdoc/>
    public void Clear()
    {
        _history.Clear();
        _redoHistory.Clear();
        InkCanvas.Strokes.Clear();
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

        _ignoreStrokesChange = true;
        var lastItem = _redoHistory.Pop();
        if (lastItem.Type == StrokesHistoryNodeType.Removed)
        {
            InkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            InkCanvas.Strokes.Add(lastItem.Strokes);
        }

        _ignoreStrokesChange = false;
        Push(lastItem);
    }

    /// <inheritdoc/>
    public void Undo()
    {
        if (!CanUndo())
        {
            return;
        }

        _ignoreStrokesChange = true;
        var lastItem = Pop();
        if (lastItem.Type == StrokesHistoryNodeType.Added)
        {
            InkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            InkCanvas.Strokes.Add(lastItem.Strokes);
        }

        _ignoreStrokesChange = false;
        _redoHistory.Push(lastItem);
    }
}
