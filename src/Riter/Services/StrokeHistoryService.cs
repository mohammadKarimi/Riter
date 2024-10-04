using System.Windows.Controls;
using System.Windows.Ink;
using System.Xml.Linq;
using Riter.Core;
using Riter.Core.Enum;
using Riter.Core.Interfaces;

namespace Riter.Services;

/// <inheritdoc/>
internal class StrokeHistoryService : IStrokeHistoryService
{
    private readonly Stack<StrokesHistoryNode> _history = [];
    private readonly Stack<StrokesHistoryNode> _redoHistory = [];
    private bool _ignoreStrokesChange;

    private InkCanvas _inkCanvas { get; set; }

    /// <inheritdoc/>
    public void SetMainElementToRedoAndUndo(InkCanvas canvas)
    {
        _inkCanvas = canvas;
        _inkCanvas.Strokes.StrokesChanged += StrokesChanged;
    }

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

        _ignoreStrokesChange = true;
        var lastItem = _redoHistory.Pop();
        if (lastItem.Type == StrokesHistoryNodeType.Removed)
        {
            _inkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            _inkCanvas.Strokes.Add(lastItem.Strokes);
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
            _inkCanvas.Strokes.Remove(lastItem.Strokes);
        }
        else
        {
            _inkCanvas.Strokes.Add(lastItem.Strokes);
        }

        _ignoreStrokesChange = false;
        _redoHistory.Push(lastItem);
    }

    /// <summary>
    /// Handles the StrokesChanged event when the user draws on the InkCanvas.
    /// This method will be used to track and store stroke changes in a stack for history purposes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains the stroke collection that has changed.</param>
    private void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {
        if (_ignoreStrokesChange)
        {
            return;
        }

        if (e.Added.Count != 0)
        {
            Push(StrokesHistoryNode.CreateAddedType(e.Added));
        }

        if (e.Removed.Count != 0)
        {
            Push(StrokesHistoryNode.CreateRemovedType(e.Removed));
        }

        ClearRedoHistory();
    }
}
