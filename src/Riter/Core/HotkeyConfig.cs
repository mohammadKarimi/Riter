namespace Riter.Core;

public record HotkeyConfig(string Modifiers, string Key, string Description);

public class HotkeysConfig
{
    public Dictionary<string, HotkeyConfig> Hotkeys { get; set; }
}
