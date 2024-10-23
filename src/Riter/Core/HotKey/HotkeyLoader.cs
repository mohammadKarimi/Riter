using Microsoft.Extensions.Options;

namespace Riter.Core;

public class HotkeyLoader(IOptions<AppSettings> options)
{
    public Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)> LoadsAndAttach(BaseViewModel viewModel)
    => LoadHotkeys(viewModel, options.Value.HotkeysConfig);

    private static uint GetModifierKey(string modifier) => modifier switch
    {
        "CTRL" => GlobalHotkeyManager.CTRL,
        "SHIFT" => GlobalHotkeyManager.SHIFT,
        "ALT" => GlobalHotkeyManager.ALT,
        _ => 0
    };

    private static uint GetKeyValue(string key) => key.ToUpper()[0];

    /// <summary>
    /// Loads hotkeys from the appsettings.json file.
    /// </summary>
    /// <param name="viewModel">The view model that handles hotkey actions.</param>
    /// <returns>A dictionary of HotKey, Modifier, and Action for each hotkey.</returns>
    private Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)> LoadHotkeys(BaseViewModel viewModel, HotkeysConfig hotkeysConfig)
    {
        var hotkeyDictionary = new Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)>();

        foreach (var hotkey in hotkeysConfig.Hotkeys)
        {
            var hotKey = System.Enum.Parse<HotKey>(hotkey.Key);
            var modifiers = GetModifierKey(hotkey.Value.Modifiers);
            var key = GetKeyValue(hotkey.Value.Key);

            hotkeyDictionary[hotKey] = (modifiers, key, viewModel.HandleHotkey);
        }

        return hotkeyDictionary;
    }
}
