namespace Riter.Core;

public abstract class BaseViewModel
{
    /// <summary>
    /// Decides which method to call based on the hotkey pressed.
    /// </summary>
    /// <param name="hotKey">The hotkey that was pressed.</param>
    public abstract void HandleHotkey(HotKey hotKey);
}
