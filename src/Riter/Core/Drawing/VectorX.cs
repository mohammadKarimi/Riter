namespace Riter.Core.Drawing;

public readonly struct VectorX(double x, double y)
{
    public readonly double X = x;
    public readonly double Y = y;

    public static VectorX operator -(VectorX a, VectorX b) => new(b.X - a.X, b.Y - a.Y);

    public static VectorX operator +(VectorX a, VectorX b) => new(a.X + b.X, a.Y + b.Y);

    public static VectorX operator *(VectorX a, double d) => new(a.X * d, a.Y * d);

    public readonly Point ToPoint() => new(X, Y);

    public override readonly string ToString() => string.Format("[{0}, {1}]", X, Y);
}
