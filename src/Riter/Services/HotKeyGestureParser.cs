namespace Riter.Services;
public static class HotKeyGestureParser
{
    public static bool TryParse(string combination, out Key key, out ModifierKeys modifiers)
    {
        key = Key.None;
        modifiers = ModifierKeys.None;

        if (string.IsNullOrWhiteSpace(combination))
        {
            return false;
        }

        foreach (string token in combination.Split(" + "))
        {
            if (token.Equals("CTRL", StringComparison.OrdinalIgnoreCase))
            {
                modifiers |= ModifierKeys.Control;
            }
            else if (token.Equals("SHIFT", StringComparison.OrdinalIgnoreCase))
            {
                modifiers |= ModifierKeys.Shift;
            }
            else if (token.Equals("ALT", StringComparison.OrdinalIgnoreCase))
            {
                modifiers |= ModifierKeys.Alt;
            }
            else if (Enum.TryParse(token, out Key parsedKey))
            {
                key = parsedKey;
            }
        }

        return key != Key.None;
    }
}
