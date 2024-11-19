namespace Riter.Core;
public static class CursorHelper
{
    public static readonly DependencyProperty BoundCursorProperty =
        DependencyProperty.RegisterAttached(
            "BoundCursor",
            typeof(Cursor),
            typeof(CursorHelper),
            new PropertyMetadata(null, OnBoundCursorChanged));

    public static void SetBoundCursor(UIElement element, Cursor value)
    {
        element.SetValue(BoundCursorProperty, value);
    }

    public static Cursor GetBoundCursor(UIElement element)
        => (Cursor)element.GetValue(BoundCursorProperty);

    private static void OnBoundCursorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element)
        {
            element.Cursor = (Cursor)e.NewValue;
        }
    }
}
