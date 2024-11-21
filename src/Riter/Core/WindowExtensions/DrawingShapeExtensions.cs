using Riter.Core.Enum;

namespace Riter.Core.WindowExtensions;

public static class DrawingShapeExtensions
{
    public static DrawingShape ToDrawingShape(this string shapeId) => (DrawingShape)byte.Parse(shapeId);
}
