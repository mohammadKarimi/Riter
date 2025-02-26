namespace Riter.Services;
public class HotKeyCommandService(AppSettings appSettings)
{
    private readonly AppSettings _appSettings = appSettings ?? throw new ArgumentNullException(nameof(appSettings));
    private readonly Dictionary<HotKey, Action> _hotKeyCommandMap = [];

    public void InitializeCommands(Dictionary<HotKey, Action> commandMap)
    {
        ArgumentNullException.ThrowIfNull(commandMap);
        _hotKeyCommandMap.Clear();
        foreach (KeyValuePair<HotKey, Action> entry in commandMap)
        {
            _hotKeyCommandMap[entry.Key] = entry.Value;
        }
    }

    public void ExecuteHotKey(HotKeiesPressed hotKeies)
    {
        string keyCombination = BuildKeyCombination(hotKeies);
        HotKeysConfig hotkeyConfig = _appSettings.HotKeysConfig.FirstOrDefault(x => x.Value == keyCombination);

        if (hotkeyConfig != null && Enum.TryParse(hotkeyConfig.Key, out HotKey hotKeyEnum))
        {
            if (_hotKeyCommandMap.TryGetValue(hotKeyEnum, out Action command))
            {
                command.Invoke();
            }
        }
    }

    private static string BuildKeyCombination(HotKeiesPressed hotKeies)
    {
        List<string> keys = [];
        if (hotKeies.CtrlPressed) keys.Add("CTRL");
        if (hotKeies.ShiftPressed) keys.Add("SHIFT");
        keys.Add(hotKeies.Key.ToString().ToUpper());
        return string.Join(" + ", keys);
    }
}
