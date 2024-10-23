using Microsoft.Extensions.Options;
using Riter.ViewModel;

namespace Riter.Core;

public class HotkeyFinders
{
    private readonly Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)> _hotkeys;
    private GlobalHotkeyManager _globalHotkeyManager;

    public HotkeyFinders(PalleteStateViewModel viewModel, IOptions<AppSettings> options)
    {
        _hotkeys = LoadHotkeys(viewModel, options.Value.HotkeysConfig);
    }

    /// <summary>
    /// Registers hotkeys in the GlobalHotkeyManager.
    /// </summary>
    /// <param name="window">The main window that will listen for the hotkeys.</param>
    public void RegisterHotkeys(Window window)
    {
        _globalHotkeyManager = new GlobalHotkeyManager(window);

        foreach (var hotkey in _hotkeys)
        {
            _globalHotkeyManager.RegisterHotkey(hotkey.Key, hotkey.Value.modifiers, hotkey.Value.key, hotkey.Value.callback);
        }
    }

    /// <summary>
    /// Dispose the GlobalHotkeyManager when closing the application.
    /// </summary>
    public void Dispose() => _globalHotkeyManager?.Dispose();

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
    private Dictionary<HotKey, (uint modifiers, uint key, Action<HotKey> callback)> LoadHotkeys(PalleteStateViewModel viewModel, HotkeysConfig hotkeysConfig)
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
