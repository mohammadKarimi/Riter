using System.Text;

namespace Riter.Services;
public class HotKeyCommandService(AppSettings appSettings)
{
    private readonly AppSettings _appSettings = appSettings;
    private Dictionary<HotKey, Action> _hotKeyCommandMap;

    public void InitializeCommands(Dictionary<HotKey, Action> commandMap) => _hotKeyCommandMap = commandMap;

    public void ExecuteHotKey(HotKeiesPressed hotKeies)
    {
        string keiesMap = BuildKeyCombination(hotKeies);
        HotKeysConfig hotkey = _appSettings.HotKeysConfig.FirstOrDefault(x => x.Value == keiesMap);

        if (hotkey is null || !Enum.TryParse(hotkey.Key, out HotKey hotKeyEnum))
        {
            return;
        }

        if (_hotKeyCommandMap.TryGetValue(hotKeyEnum, out Action command))
        {
            command.Invoke();
        }
    }

    private static string BuildKeyCombination(HotKeiesPressed hotKeies)
    {
        StringBuilder keiesMap = new();

        if (hotKeies.CtrlPressed)
        {
            keiesMap.Append("CTRL + ");
        }

        if (hotKeies.ShiftPressed)
        {
            keiesMap.Append("SHIFT + ");
        }

        keiesMap.Append(hotKeies.Key.ToString().ToUpper());
        return keiesMap.ToString();
    }
}
