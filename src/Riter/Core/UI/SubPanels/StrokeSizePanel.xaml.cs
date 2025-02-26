using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Riter.Core.UI.SubPanels;

/// <summary>
/// Interaction logic for StrokeSizePanel.xaml.
/// </summary>
public partial class StrokeSizePanel : UserControl
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StrokeSizePanel"/> class.
    /// </summary>
    public StrokeSizePanel()
    {
        InitializeComponent();
        AppSettings appSetting = App.ServiceProvider.GetService<AppSettings>();

        Dictionary<string, string> hotkeys = appSetting.HotKeysConfig.ToDictionary(x => x.Key, x => x.Value);
        Stroke07XHotKey.Content = hotkeys[HotKey.SizeOfBrush07X.ToString()].Replace("D1", "1");
        Stroke1XHotKey.Content = hotkeys[HotKey.SizeOfBrush1X.ToString()].Replace("D2", "2");
        Stroke2XHotKey.Content = hotkeys[HotKey.SizeOfBrush2X.ToString()].Replace("D3", "3");
        Stroke3XHotKey.Content = hotkeys[HotKey.SizeOfBrush3X.ToString()].Replace("D4", "4");
    }
}
