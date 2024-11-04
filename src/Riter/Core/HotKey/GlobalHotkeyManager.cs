//using System.Runtime.InteropServices;
//using System.Windows.Interop;

//namespace Riter.Core;

///// <summary>
///// Provides a mechanism for registering and managing multiple global hotkeys in a WPF application.
///// Allows triggering specific actions using keyboard shortcuts, even when the application is not in focus.
///// </summary>
//public partial class GlobalHotKeyManager : IDisposable
//{
//    private readonly IntPtr _windowHandle;
//    private readonly HwndSource _source;
//    private readonly Dictionary<HotKey, Action<HotKey>> _hotkeyActions = [];

//    private bool _isHookAdded;

//    /// <summary>
//    /// Initializes a new instance of the <see cref="GlobalHotKeyManager"/> class for the specified WPF window.
//    /// </summary>
//    /// <param name="window">The WPF window that the global hotkeys will be tied to.</param>
//    public GlobalHotKeyManager(Window window)
//    {
//        _windowHandle = new WindowInteropHelper(window).Handle;
//        _source = HwndSource.FromHwnd(_windowHandle);
//    }

//    public bool RegisterHotkey(HotKey id, uint modifiers, uint key, Action<HotKey> callback)
//    {
//        if (!_isHookAdded)
//        {
//            _source.AddHook(HwndHook);
//            _isHookAdded = true;
//        }

//        var registered = RegisterHotKey(_windowHandle, (int)id, modifiers, key);
//        if (registered)
//        {
//            _hotkeyActions[id] = callback;
//        }

//        return registered;
//    }


//    /// <summary>
//    /// Unregisters a hotkey by its unique identifier.
//    /// </summary>
//    /// <param name="id">The unique identifier of the hotkey to unregister.</param>
//    public void UnregisterHotkey(HotKey id)
//    {
//        if (_hotkeyActions.ContainsKey(id))
//        {
//            UnregisterHotKey(_windowHandle, (int)id);
//            _hotkeyActions.Remove(id);
//        }
//    }

//    /// <summary>
//    /// Releases all resources used by the <see cref="GlobalHotkeyManager"/> class.
//    /// Unregisters all registered hotkeys and removes the window hook.
//    /// </summary>
//    public void Dispose()
//    {
//        foreach (var id in _hotkeyActions.Keys)
//        {
//            UnregisterHotKey(_windowHandle, (int)id);
//        }

//        _source.RemoveHook(HwndHook);
//        _hotkeyActions.Clear();
//    }

//    [LibraryImport("user32.dll")]
//    [return: MarshalAs(UnmanagedType.Bool)]
//    private static partial bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

//    [LibraryImport("user32.dll")]
//    [return: MarshalAs(UnmanagedType.Bool)]
//    private static partial bool UnregisterHotKey(IntPtr hWnd, int id);

//    [LibraryImport("user32.dll", SetLastError = true)]
//    private static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

//    private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
//    {
//        const int wM_HOTKEY = 0x0312;

//        if (msg == wM_HOTKEY)
//        {
//            var id = (HotKey)wParam.ToInt32();

//            if (_hotkeyActions.TryGetValue(id, out var value))
//            {
//                value?.Invoke(id);
//                handled = true;
//            }
//        }

//        return CallNextHookEx(IntPtr.Zero, msg, wParam, lParam);
//    }
//}
