using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Riter.Core;

/// <summary>
/// Provides a mechanism for registering and managing multiple global hotkeys in a WPF application.
/// Allows triggering specific actions using keyboard shortcuts, even when the application is not in focus.
/// </summary>
public partial class GlobalHotkeyManager : IDisposable
{
    public const uint CTRL = 0x0002;
    public const uint SHIFT = 0x0004;
    public const uint ALT = 0x0001;

    private readonly IntPtr _windowHandle;
    private readonly HwndSource _source;
    private readonly Dictionary<HotKey, Action<HotKey>> _hotkeyActions = [];

    private bool _isHookAdded;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalHotkeyManager"/> class for the specified WPF window.
    /// </summary>
    /// <param name="window">The WPF window that the global hotkeys will be tied to.</param>
    public GlobalHotkeyManager(Window window)
    {
        _windowHandle = new WindowInteropHelper(window).Handle;
        _source = HwndSource.FromHwnd(_windowHandle);
    }

    /// <summary>
    /// Registers a global hotkey for the specified key combination.
    /// </summary>
    /// <param name="id">A unique identifier for the hotkey.</param>
    /// <param name="modifiers">Key modifiers such as <c>Ctrl</c>, <c>Alt</c>, or <c>Shift</c>.</param>
    /// <param name="key">The virtual key code for the hotkey.</param>
    /// <param name="callback">The method to invoke when the hotkey is pressed.</param>
    /// <returns><c>true</c> if the hotkey is successfully registered; otherwise, <c>false</c>.</returns>
    public bool RegisterHotkey(HotKey id, uint modifiers, uint key, Action<HotKey> callback)
    {
        if (!_isHookAdded)
        {
            _source.AddHook(HwndHook);
            _isHookAdded = true;
        }

        var registered = RegisterHotKey(_windowHandle, (int)id, modifiers, key);
        if (registered)
        {
            _hotkeyActions[id] = callback;
        }

        return registered;
    }

    /// <summary>
    /// Unregisters a hotkey by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the hotkey to unregister.</param>
    public void UnregisterHotkey(HotKey id)
    {
        if (_hotkeyActions.ContainsKey(id))
        {
            UnregisterHotKey(_windowHandle, (int)id);
            _hotkeyActions.Remove(id);
        }
    }

    /// <summary>
    /// Releases all resources used by the <see cref="GlobalHotkeyManager"/> class.
    /// Unregisters all registered hotkeys and removes the window hook.
    /// </summary>
    public void Dispose()
    {
        foreach (var id in _hotkeyActions.Keys)
        {
            UnregisterHotKey(_windowHandle, (int)id);
        }

        _source.RemoveHook(HwndHook);
        _hotkeyActions.Clear();
    }

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool UnregisterHotKey(IntPtr hWnd, int id);

    /// <summary>
    /// Processes window messages for registered hotkeys.
    /// Invokes the associated callback action when a hotkey is pressed.
    /// </summary>
    /// <param name="hwnd">The handle to the window receiving the message.</param>
    /// <param name="msg">The message identifier.</param>
    /// <param name="wParam"> wParam Additional message information.</param>
    /// <param name="lParam"> lParam Additional message information.</param>
    /// <param name="handled">Indicates whether the message has been handled.</param>
    /// <returns>The return value is zero if the message is handled.</returns>
    private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
    {
        const int wM_HOTKEY = 0x0312;

        if (msg == wM_HOTKEY)
        {
            var id = (HotKey)wParam.ToInt32();

            if (_hotkeyActions.TryGetValue(id, out var value))
            {
                value?.Invoke(id);
                handled = true;
            }
        }

        return IntPtr.Zero;
    }
}
