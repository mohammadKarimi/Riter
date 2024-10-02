using Riter.Main.Core;
using Riter.Main.Core.Enum;

namespace Riter.Main.ViewModel;

/// <inheritdoc/>
internal class PalleteStorkeHistoryState : IStrokeHistoryService
{
    private readonly Stack<StrokesHistoryNode> _history = [];
    private readonly Stack<StrokesHistoryNode> _redoHistory = [];

    /// <inheritdoc/>
    public bool CanRedo() => _redoHistory.Count != 0;

    /// <inheritdoc/>
    public bool CanUndo() => _history.Count != 0;

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

        var lastItem = PopFromRedoCollection();
        if (lastItem.Type == StrokesHistoryNodeType.Removed)
        {
            // MainInkCanvas Remove
        }
        else
        {
            // MainInkCanvas Add
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
            // Remove to MainInkCanvas
        }
        else
        {
            // Add to MainInkCanvas
        }

        PushToRedoCollection(lastItem);
    }

    private StrokesHistoryNode PopFromRedoCollection() => _redoHistory.Pop();

    private void PushToRedoCollection(StrokesHistoryNode node) => _redoHistory.Push(node);
}
