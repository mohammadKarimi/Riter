using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Controls;
using System.Windows.Ink;
using Riter.Core.Extensions;
using Riter.Core.Interfaces;
using Riter.Core.UI;
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

    private readonly IStrokeHistoryService _strokeHistoryService;

    public MainWindow(
        PaletteStateOrchestratorViewModel orchestratorViewModel,
        IStrokeHistoryService strokeHistoryService)
    {
        InitializeComponent();
        DataContext = orchestratorViewModel;
        _orchestratorViewModel = orchestratorViewModel;
        _strokeHistoryService = strokeHistoryService;
        _strokeHistoryService.SetMainElementToRedoAndUndo(MainInkCanvasControl.MainInkCanvas);
        MainInkCanvasControl.MainInkCanvas.Strokes.StrokesChanged += StrokesChanged;

        this.EnableDragging(MainPalette)
            .SetTopMost(true);

        Loaded += MainWindow_Loaded;
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
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc)
    {
        using var curProcess = Process.GetCurrentProcess();
        using var curModule = curProcess.MainModule;
        return SetWindowsHookEx(WHKEYBOARDLL, proc, GetModuleHandle(curModule.ModuleName), 0);
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        var isKey = true;
        if (nCode >= 0 && (wParam == WMKEYDOWN || wParam == WMKEYUP))
        {
            var vkCode = Marshal.ReadInt32(lParam);

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
    /// Handles the StrokesChanged event when the user draws on the InkCanvas.
    /// This method will be used to track and store stroke changes in a stack for history purposes.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">Contains the stroke collection that has changed.</param>
    private void StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {
        if (_strokeHistoryService.IgnoreStrokesChange)
        {
            return;
        }

        if (e.Added.Count != 0)
        {
            Console.WriteLine(e.ToString());
            _strokeHistoryService.Push(StrokesHistoryNode.CreateAddedType(e.Added));
        }

        if (e.Removed.Count != 0)
        {
            _strokeHistoryService.Push(StrokesHistoryNode.CreateRemovedType(e.Removed));
        }
    }

    /// <summary>
    /// Load the window in bottom center of screen.
    /// </summary>
    /// <param name="e">Contains the data of routed event.</param>
    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        AdjustWindowSize();
        Microsoft.Win32.SystemEvents.DisplaySettingsChanged += (_, _) => AdjustWindowSize();
    }

    private void AdjustWindowSize()
    {
        var canvasWidth = Layout.ActualWidth;
        var canvasHeight = Layout.ActualHeight;
        var paletteWidth = MainPalette.ActualWidth;
        var paletteHeight = MainPalette.ActualHeight;

        if (MainPalette != null)
        {
            Canvas.SetLeft(MainPalette, (canvasWidth - paletteWidth) / 2);
            Canvas.SetTop(MainPalette, canvasHeight - paletteHeight - 75);
        }
    }
}
