using Riter.ViewModel;

namespace Riter.Core;

public class HotKeyLoader(AppSettings options)
{
    public const uint CTRL = 0x0002;
    public const uint SHIFT = 0x0004;
    public const uint ALT = 0x0001;

    public Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)> Loads(PaletteStateOrchestratorViewModel viewModel)
    {
        Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)> hotkeys = [];
        foreach (var hotkey in options.HotKeysConfig)
        {
            var hotKey = System.Enum.Parse<HotKey>(hotkey.Key);
            var modifier = GetModifierKey(hotkey.Value.Modifiers);
            var key = GetKeyValue(hotkey.Value.Key);

            hotkeys[hotKey] = (modifier, key, viewModel.HandleHotkey);
        }

        return hotkeys;
    }

    private static uint GetModifierKey(string modifier) => modifier switch
    {
        "CTRL" => CTRL,
        "SHIFT" => SHIFT,
        "ALT" => ALT,
        _ => 0
    };

    private static uint GetKeyValue(string key) => key.ToUpper()[0];
}
