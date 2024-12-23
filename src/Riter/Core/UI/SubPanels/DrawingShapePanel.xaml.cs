using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Riter.Core.UI.SubPanels;

/// <summary>
/// Interaction logic for DrawingShapePanel.xaml.
/// </summary>
public partial class DrawingShapePanel : UserControl
{
    public DrawingShapePanel()
    {
        InitializeComponent();
        var appSetting = App.ServiceProvider.GetService<AppSettings>();

        var hotkeys = appSetting.HotKeysConfig.ToDictionary(x => x.Key, x => x.Value);
        ArrowHotKey.Content = hotkeys[HotKey.Arrow.ToString()];
        LineHotKey.Content = hotkeys[HotKey.Line.ToString()];
        RectangleHotKey.Content = hotkeys[HotKey.Rectangle.ToString()];
        CircleHotKey.Content = hotkeys[HotKey.Circle.ToString()];
        DatabaseHotKey.Content = hotkeys[HotKey.Database.ToString()];
        FilledCircleHotKey.Content = hotkeys[HotKey.FilledCircle.ToString()];
        FilledRectangleHotKey.Content = hotkeys[HotKey.FilledRectangle.ToString()];
    }
}
