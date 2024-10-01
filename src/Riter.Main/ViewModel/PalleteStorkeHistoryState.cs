using Riter.Main.Core;

namespace Riter.Main.ViewModel;

/// <inheritdoc/>
internal class PalleteStorkeHistoryState : IStrokeHistoryService
{
    private readonly Stack<StrokesHistoryNode> _history = [];
    private readonly Stack<StrokesHistoryNode> _redoHistory = [];

    /// <inheritdoc/>
    public bool CanRedo() => throw new NotImplementedException();

    /// <inheritdoc/>
    public bool CanUndo() => throw new NotImplementedException();

    /// <inheritdoc/>
    public StrokesHistoryNode Pop(Stack<StrokesHistoryNode> collection) => collection.Count == 0 ? null : collection.Pop();

    /// <inheritdoc/>
    public void Push(Stack<StrokesHistoryNode> collection, StrokesHistoryNode node) => collection.Push(node);

    /// <inheritdoc/>
    public void Redo() => throw new NotImplementedException();

    /// <inheritdoc/>
    public void Undo() => throw new NotImplementedException();
}
