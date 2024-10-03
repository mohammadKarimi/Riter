using System.Windows.Controls;

namespace Riter.Main.Core.Interfaces;

/// <summary>
/// Provides a mechanism to manage the history of strokes (drawings) on an InkCanvas.
/// Supports undo and redo operations by maintaining a history of stroke changes.
/// </summary>
public interface IStrokeHistoryService
{

    /// <summary>
    /// Config the main element for adding redo and undo functionality.
    /// </summary>
    /// <param name="canvas">Ink Canvas for Redo/Undo.</param>
    void SetMainElementToRedoAndUndo(InkCanvas canvas);

    /// <summary>
    /// Reverts the last stroke action (undo) if possible.
    /// Removes the last stroke from the history and applies the change.
    /// </summary>
    void Undo();

    /// <summary>
    /// Reapplies the last undone stroke action (redo) if possible.
    /// Re-adds the previously undone stroke to the canvas.
    /// </summary>
    void Redo();

    /// <summary>
    /// Adds a new stroke to the specified stroke history stack.
    /// This is typically used to store a stroke action after it has been performed.
    /// </summary>
    /// <param name="node">The stroke history node representing the stroke action to be stored.</param>
    void Push(StrokesHistoryNode node);

    /// <summary>
    /// Removes and returns the last stroke action from the specified stroke history stack.
    /// Typically used to undo or redo a stroke action.
    /// </summary>
    /// <returns>The stroke history node representing the last stroke action in the stack.</returns>
    StrokesHistoryNode Pop();

    /// <summary>
    /// Determines whether there are any actions available to undo.
    /// </summary>
    /// <returns>True if there are actions that can be undone; otherwise, false.</returns>
    bool CanUndo();

    /// <summary>
    /// Determines whether there are any actions available to redo.
    /// </summary>
    /// <returns>True if there are actions that can be redone; otherwise, false.</returns>
    bool CanRedo();

    /// <summary>
    /// Clear the collection of history and redo.
    /// </summary>
    void Clear();

    /// <summary>
    /// Remove all redo collections.
    /// </summary>
    void ClearRedoHistory();
}
