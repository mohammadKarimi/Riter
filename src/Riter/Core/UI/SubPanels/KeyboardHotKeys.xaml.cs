using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Riter.Core.UI.SubPanels;

/// <summary>
/// Interaction logic for KeyboardHotKeys.xaml.
/// </summary>
public partial class KeyboardHotKeys : UserControl
{
    private readonly AppSettings _settings;
    private readonly Dictionary<string, string> _hotkeys = [];

    public KeyboardHotKeys()
    {
        InitializeComponent();

        _settings = App.ServiceProvider.GetService<AppSettings>();
        _hotkeys = _settings.HotKeysConfig.ToDictionary(x => x.Key, x => x.Value);
        Drawing.Text = _hotkeys[HotKey.Drawing.ToString()];
        Erasing.Text = _hotkeys[HotKey.Erasing.ToString()];
        Undo.Text = _hotkeys[HotKey.Undo.ToString()];
        Redo.Text = _hotkeys[HotKey.Red.ToString()];
        Highlighter.Text = _hotkeys[HotKey.Highlighter.ToString()];
        Release.Text = _hotkeys[HotKey.Release.ToString()];
        HideAll.Text = _hotkeys[HotKey.HideAll.ToString()];
        Trash.Text = _hotkeys[HotKey.Trash.ToString()];
        SizeOfBrush1X.Text = _hotkeys[HotKey.SizeOfBrush1X.ToString()];
        SizeOfBrush2X.Text = _hotkeys[HotKey.SizeOfBrush2X.ToString()];
        SizeOfBrush3X.Text = _hotkeys[HotKey.SizeOfBrush3X.ToString()];
        TransparentBackground.Text = _hotkeys[HotKey.TransparentBackground.ToString()];
        WhiteboardBackground.Text = _hotkeys[HotKey.WhiteboardBackground.ToString()];
        BlackboardBackground.Text = _hotkeys[HotKey.BlackboardBackground.ToString()];

    }
}
