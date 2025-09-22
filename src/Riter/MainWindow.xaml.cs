using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using Riter.Core.WindowExtensions;
using Riter.ViewModel;

namespace Riter;

/// <summary>
/// Initializes a new instance of the <see cref="MainWindow"/> class.
/// </summary>
public partial class MainWindow : Window
{
    private const int WHKEYBOARDLL = 13;
    private const int WMKEYDOWN = 0x0100;
    private const int WMKEYUP = 0x0101;

    private static readonly LowLevelKeyboardProc _proc = HookCallback;
    private static IntPtr _hookID = IntPtr.Zero;
    private static bool _ctrlPressed = false;
    private static bool _shiftPressed = false;
    private static PaletteStateOrchestratorViewModel _orchestratorViewModel;

    private readonly AppSettings _appSettings;

    public MainWindow(
        PaletteStateOrchestratorViewModel orchestratorViewModel,
        AppSettings appSettings)
    {
        InitializeComponent();
        DataContext = orchestratorViewModel;
        _orchestratorViewModel = orchestratorViewModel;
        _appSettings = appSettings;

        this.EnableDragging(MainPalette)
            .SetTopMost(true);

        Loaded += MainWindow_Loaded;
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        UnhookWindowsHookEx(_hookID);
    }

    /// <inheritdoc/>
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        _hookID = SetHook(_proc);

        // Set the window to cover the entire virtual desktop area
        SetWindowToVirtualDesktop();
    }

    private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using Process curProcess = Process.GetCurrentProcess();
        using ProcessModule curModule = curProcess.MainModule;
        return SetWindowsHookEx(WHKEYBOARDLL, proc, GetModuleHandle(curModule.ModuleName), 0);
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        bool isKey = true;
        if (nCode >= 0 && (wParam == WMKEYDOWN || wParam == WMKEYUP))
        {
            int vkCode = Marshal.ReadInt32(lParam);

            if (wParam is WMKEYDOWN)
            {
                if (vkCode == KeyInterop.VirtualKeyFromKey(Key.LeftCtrl) || vkCode == KeyInterop.VirtualKeyFromKey(Key.RightCtrl))
                {
                    _ctrlPressed = true;
                    isKey = false;
                }

                if (vkCode == KeyInterop.VirtualKeyFromKey(Key.LeftShift) || vkCode == KeyInterop.VirtualKeyFromKey(Key.RightShift))
                {
                    _shiftPressed = true;
                    isKey = false;
                }

                if (isKey)
                {
                    _orchestratorViewModel.HandleHotkey(new HotKeiesPressed(KeyInterop.KeyFromVirtualKey(vkCode).ToString(), _ctrlPressed, _shiftPressed));
                    _ctrlPressed = false;
                    _shiftPressed = false;
                }
            }
            else if (wParam == WMKEYUP)
            {
                if (vkCode == KeyInterop.VirtualKeyFromKey(Key.LeftCtrl) || vkCode == KeyInterop.VirtualKeyFromKey(Key.RightCtrl))
                {
                    _ctrlPressed = false;
                }

                if (vkCode == KeyInterop.VirtualKeyFromKey(Key.LeftShift) || vkCode == KeyInterop.VirtualKeyFromKey(Key.RightShift))
                {
                    _shiftPressed = false;
                }
            }
        }

        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }

    /// <summary>
    /// Load the window in bottom center of screen.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">Contains the data of routed event.</param>
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        SetWindowToVirtualDesktop();
        AdjustWindowSize();
        Microsoft.Win32.SystemEvents.DisplaySettingsChanged += (_, _) =>
        {
            SetWindowToVirtualDesktop();
            AdjustWindowSize();
        };
    }

    /// <summary>
    /// Sets the window to cover the entire virtual desktop area across all monitors.
    /// </summary>
    private void SetWindowToVirtualDesktop()
    {
        // Set window position and size to cover the entire virtual desktop
        this.Left = SystemParameters.VirtualScreenLeft;
        this.Top = SystemParameters.VirtualScreenTop;
        this.Width = SystemParameters.VirtualScreenWidth;
        this.Height = SystemParameters.VirtualScreenHeight;

        // Ensure the layout grid fills the entire window
        if (Layout != null)
        {
            Layout.Width = this.Width;
            Layout.Height = this.Height;
        }
    }

    private void AdjustWindowSize()
    {
        if (MainPalette != null)
        {
            StartupLocationContext context = new(_appSettings.StartupLocation);
            context.ExecuteStrategy(Layout, MainPalette, _appSettings);
        }
    }
}
