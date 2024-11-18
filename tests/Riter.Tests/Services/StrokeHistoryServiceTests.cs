using System.Windows.Ink;
using Moq;
using Riter.Core;
using Riter.Core.Enum;
using Riter.Services;

namespace Riter.Tests.Services;
public class StrokeHistoryServiceTests
{
    private readonly StrokeHistoryService _service;
    private readonly Mock<InkCanvas> _mockInkCanvas;

    public StrokeHistoryServiceTests()
    {
        _mockInkCanvas = new Mock<InkCanvas>();
        _service = new StrokeHistoryService();
    }

    private static StrokesHistoryNode CreateHistoryNode(StrokesHistoryNodeType type, StrokeCollection strokes = null)
        => type == StrokesHistoryNodeType.Added
            ? StrokesHistoryNode.CreateAddedType(strokes)
            : StrokesHistoryNode.CreateAddedType(strokes);

    [Fact]
    public void Push_ShouldAddHistoryNode_ToUndoStack()
    {
        var node = CreateHistoryNode(StrokesHistoryNodeType.Added);

        _service.Push(node);
        var result = _service.Pop();

        Assert.Equal(node, result);
    }

    [Fact]
    public void CanUndo_ShouldReturnTrue_WhenHistoryStackIsNotEmpty()
    {
        var node = CreateHistoryNode(StrokesHistoryNodeType.Added);
        _service.Push(node);

        Assert.True(_service.CanUndo());
    }

    [Fact]
    public void CanUndo_ShouldReturnFalse_WhenHistoryStackIsEmpty() => Assert.False(_service.CanUndo());

    
    public void CanRedo_ShouldReturnTrue_WhenRedoStackIsNotEmpty()
    {
        var node = CreateHistoryNode(StrokesHistoryNodeType.Added);
        _service.Push(node);
        _service.Undo();

        Assert.True(_service.CanRedo());
    }

    [Fact]
    public void CanRedo_ShouldReturnFalse_WhenRedoStackIsEmpty() => Assert.False(_service.CanRedo());

    public void Undo_ShouldMoveNode_FromHistoryToRedoStack()
    {
        var strokes = new StrokeCollection();
        var node = CreateHistoryNode(StrokesHistoryNodeType.Added, strokes);

        _service.Push(node);
        _service.Undo();

        Assert.False(_service.CanUndo());
        Assert.True(_service.CanRedo());
    }

    public void Undo_ShouldRemoveStrokes_FromInkCanvas_WhenNodeTypeIsAdded()
    {
        var strokes = new StrokeCollection();
        var node = CreateHistoryNode(StrokesHistoryNodeType.Added, strokes);

        _service.Push(node);
        _mockInkCanvas.Setup(c => c.Strokes).Returns(strokes);

        _service.Undo();

        _mockInkCanvas.Verify(c => c.Strokes.Remove(strokes), Times.Once);
    }

    public void Redo_ShouldMoveNode_FromRedoToHistoryStack()
    {
        var strokes = new StrokeCollection();
        var node = CreateHistoryNode(StrokesHistoryNodeType.Removed, strokes);

        _service.Push(node);
        _service.Undo();
        _service.Redo();

        Assert.False(_service.CanRedo());
        Assert.True(_service.CanUndo());
    }

    public void Redo_ShouldAddStrokes_ToInkCanvas_WhenNodeTypeIsRemoved()
    {
        var strokes = new StrokeCollection();
        var node = CreateHistoryNode(StrokesHistoryNodeType.Removed, strokes);

        _service.Push(node);
        _mockInkCanvas.Setup(c => c.Strokes).Returns(strokes);

        _service.Undo();
        _service.Redo();

        _mockInkCanvas.Verify(c => c.Strokes.Add(strokes), Times.Once);
    }

    public void Clear_ShouldClearHistory_RedoAndCanvasStrokes()
    {
        var strokes = new StrokeCollection();
        var node = CreateHistoryNode(StrokesHistoryNodeType.Added, strokes);

        _service.Push(node);
        _mockInkCanvas.Setup(c => c.Strokes).Returns(strokes);

        _service.Clear();

        Assert.False(_service.CanUndo());
        Assert.False(_service.CanRedo());
        _mockInkCanvas.Verify(c => c.Strokes.Clear(), Times.Once);
    }

    public void ClearRedoHistory_ShouldOnlyClearRedoStack()
    {
        var strokes = new StrokeCollection();
        var node = CreateHistoryNode(StrokesHistoryNodeType.Removed, strokes);

        _service.Push(node);
        _service.Undo();
        _service.ClearRedoHistory();

        Assert.False(_service.CanRedo());
        Assert.True(_service.CanUndo());
    }
}
