//using System.Windows.Ink;
//using Moq;
//using Riter.Core.Drawing;
//using Riter.Core.Enum;
//using Riter.Services;
//using Xunit;

//namespace Riter.Tests.Services;
//public class StrokeHistoryServiceTests
//{
//    private readonly StrokeHistoryService _service;
//    private readonly Mock<InkCanvas> _mockInkCanvas;

//    public StrokeHistoryServiceTests()
//    {
//        _mockInkCanvas = new Mock<InkCanvas>();
//        _service = new StrokeHistoryService();
//        _service.SetMainElementToRedoAndUndo(_mockInkCanvas.Object);
//    }

//    private static StrokesHistoryNode CreateHistoryNode(StrokesHistoryNodeType type, StrokeCollection strokes = null)
//        => type == StrokesHistoryNodeType.Added
//            ? StrokesHistoryNode.CreateAddedType(strokes, false, 500)
//            : StrokesHistoryNode.CreateAddedType(strokes, false, 500);

//    [WpfFact]
//    public void Push_ShouldAddNodeToHistory()
//    {
//        StrokesHistoryNode node = CreateHistoryNode(StrokesHistoryNodeType.Added);
//        _service.Push(node);
//        StrokesHistoryNode poppedNode = _service.Pop();
//        poppedNode.Should().BeSameAs(node, "because the node should be pushed to the undo history");
//    }

//    [WpfFact]
//    public void Clear_ShouldResetAllHistoriesAndStrokes()
//    {
//        StrokeCollection strokes = new();
//        StrokesHistoryNode node = CreateHistoryNode(StrokesHistoryNodeType.Added, strokes);
//        _service.Push(node);

//        _service.Clear();

//        _mockInkCanvas.Object.Strokes.Should().BeEmpty("because the clear operation should remove all strokes");
//        _service.CanUndo().Should().BeFalse("because the undo history should be cleared");
//        _service.CanRedo().Should().BeFalse("because the redo history should be cleared");
//    }

//    [WpfFact]
//    public void Push_ShouldAddHistoryNode_ToUndoStack()
//    {
//        StrokesHistoryNode node = CreateHistoryNode(StrokesHistoryNodeType.Added);

//        _service.Push(node);
//        StrokesHistoryNode result = _service.Pop();

//        Assert.Equal(node, result);
//    }

//    [WpfFact]
//    public void CanUndo_ShouldReturnTrue_WhenHistoryStackIsNotEmpty()
//    {
//        StrokesHistoryNode node = CreateHistoryNode(StrokesHistoryNodeType.Added);
//        _service.Push(node);

//        Assert.True(_service.CanUndo());
//    }

//    [WpfFact]
//    public void CanUndo_ShouldReturnFalse_WhenHistoryStackIsEmpty() => Assert.False(_service.CanUndo());

//    [WpfFact]
//    public void CanRedo_ShouldReturnFalse_WhenRedoStackIsEmpty() => Assert.False(_service.CanRedo());

//    [WpfFact]
//    public void Undo_ShouldMoveNode_FromHistoryToRedoStack()
//    {
//        StrokeCollection strokes = new();
//        StrokesHistoryNode node = CreateHistoryNode(StrokesHistoryNodeType.Added, strokes);

//        _service.Push(node);
//        _service.Undo();

//        Assert.False(_service.CanUndo());
//        Assert.True(_service.CanRedo());
//    }

//    [WpfFact]
//    public void Redo_ShouldMoveNode_FromRedoToHistoryStack()
//    {
//        StrokeCollection strokes = new();
//        StrokesHistoryNode node = CreateHistoryNode(StrokesHistoryNodeType.Removed, strokes);

//        _service.Push(node);
//        _service.Undo();
//        _service.Redo();

//        Assert.False(_service.CanRedo());
//        Assert.True(_service.CanUndo());
//    }

//    [WpfFact]
//    public void ClearRedoHistory_ShouldOnlyClearRedoStack()
//    {
//        StrokeCollection strokes = new();
//        StrokesHistoryNode node = CreateHistoryNode(StrokesHistoryNodeType.Added, strokes);

//        _service.Push(node);
//        _service.Undo();
//        _service.ClearRedoHistory();

//        Assert.False(_service.CanRedo());
//        Assert.False(_service.CanUndo());
//    }
//}
