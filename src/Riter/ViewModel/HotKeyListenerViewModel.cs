using Riter.Core.Enum;
using Riter.Core.IO;

namespace Riter.ViewModel;
public class HotKeyListenerViewModel(AppSettings appSettings) : BaseViewModel
{
    private readonly AppSettings _appSettings = appSettings;
    private HotKeyListenerMode _hotKeyListenerMode = appSettings.HotKeyListenerMode;

    public HotKeyListenerMode HotKeyListenerMode
    {
        get => _hotKeyListenerMode;
        set
        {
            if (_hotKeyListenerMode != value)
            {
                _hotKeyListenerMode = value;
                OnPropertyChanged(nameof(HotKeyListenerMode));
                OnPropertyChanged(nameof(CurrentListenerName));
            }
        }
    }

    /// <summary>
    /// Current hotkey listener name in string.
    /// </summary>
    public string CurrentListenerName => _hotKeyListenerMode == HotKeyListenerMode.DefaultListener
        ? "Default Listener"
        : "Low-Level Keyboard Hook";

    public ICommand ToggleCommand => new RelayCommand(() =>
    {
        HotKeyListenerMode = _hotKeyListenerMode == HotKeyListenerMode.LowLevelKeyboardHook
            ? HotKeyListenerMode.DefaultListener
            : HotKeyListenerMode.LowLevelKeyboardHook;
        _appSettings.HotKeyListenerMode = HotKeyListenerMode;
        Task.Run(async () =>
        {
            await FileStorage.SaveConfig(_appSettings).ConfigureAwait(false);
        });
    });
}
