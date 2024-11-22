using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Enum;

namespace Riter.Core.Interfaces;
public interface IShapeDrawer
{
    DrawingShape SupportedShape { get; }

    Stroke DrawShape(InkCanvas canvas, Point startPoint, Point endPoint, bool isRainbow = false);
}
