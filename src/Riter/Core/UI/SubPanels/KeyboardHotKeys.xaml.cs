using System.Text;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Riter.Core.IO;

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
        Redo.Text = _hotkeys[HotKey.Redo.ToString()];
        Highlighter.Text = _hotkeys[HotKey.Highlighter.ToString()];
        Release.Text = _hotkeys[HotKey.Release.ToString()];
        HideAll.Text = _hotkeys[HotKey.HideAll.ToString()];
        Trash.Text = _hotkeys[HotKey.Trash.ToString()];
        SizeOfBrush07X.Text = _hotkeys[HotKey.SizeOfBrush07X.ToString()];
        SizeOfBrush1X.Text = _hotkeys[HotKey.SizeOfBrush1X.ToString()];
        SizeOfBrush2X.Text = _hotkeys[HotKey.SizeOfBrush2X.ToString()];
        SizeOfBrush3X.Text = _hotkeys[HotKey.SizeOfBrush3X.ToString()];
        TransparentBackground.Text = _hotkeys[HotKey.TransparentBackground.ToString()];
        WhiteboardBackground.Text = _hotkeys[HotKey.WhiteboardBackground.ToString()];
        BlackboardBackground.Text = _hotkeys[HotKey.BlackboardBackground.ToString()];

        Database.Text = _hotkeys[HotKey.Database.ToString()];
        Arrow.Text = _hotkeys[HotKey.Arrow.ToString()];
        Line.Text = _hotkeys[HotKey.Line.ToString()];
        Circle.Text = _hotkeys[HotKey.Circle.ToString()];
        Rectangle.Text = _hotkeys[HotKey.Rectangle.ToString()];

        Yellow.Text = _hotkeys[HotKey.Yellow.ToString()];
        Purple.Text = _hotkeys[HotKey.Purple.ToString()];
        Mint.Text = _hotkeys[HotKey.Mint.ToString()];
        Coral.Text = _hotkeys[HotKey.Coral.ToString()];
        Red.Text = _hotkeys[HotKey.Red.ToString()];
        Cyan.Text = _hotkeys[HotKey.Cyan.ToString()];
        Pink.Text = _hotkeys[HotKey.Pink.ToString()];
        Gray.Text = _hotkeys[HotKey.Gray.ToString()];
        Black.Text = _hotkeys[HotKey.Black.ToString()];
        Rainbow.Text = _hotkeys[HotKey.Rainbow.ToString()];
    }

    private void UserControl_KeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.FocusedElement is not TextBox focusedTextBox)
        {
            return;
        }

        var keyCombinationBuilder = new StringBuilder();

        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
        {
            keyCombinationBuilder.Append("CTRL + ");
        }

        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
        {
            keyCombinationBuilder.Append("SHIFT + ");
        }

        if (Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
        {
            keyCombinationBuilder.Append("ALT + ");
        }

        keyCombinationBuilder.Append(e.Key.ToString().ToUpper());

        var keyCombination = keyCombinationBuilder.ToString();
        focusedTextBox.Text = keyCombination;
        e.Handled = true;

        var hotKeyConfig = _settings.HotKeysConfig.Where(x => x.Key == focusedTextBox.Name).FirstOrDefault();

        if (hotKeyConfig is not null)
        {
            hotKeyConfig.Value = keyCombination;
        }
    }

    private void UserControl_KeyUp(object sender, KeyEventArgs e)
        => Task.Run(async () =>
            {
                await FileStorage.SaveConfig(_settings).ConfigureAwait(false);
            });
}
