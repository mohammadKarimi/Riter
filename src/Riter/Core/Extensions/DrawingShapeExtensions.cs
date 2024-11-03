using Riter.Core.Enum;

namespace Riter.Core.Extensions;

public static class DrawingShapeExtensions
{
    public static DrawingShape ToDrawingShape(this string shapeId) => (DrawingShape)byte.Parse(shapeId);
}
