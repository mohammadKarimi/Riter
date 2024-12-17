namespace Riter.ViewModel;
public sealed class StrokeVisibilityViewModel : BaseViewModel
{
    private readonly ISettingPanelStateHandler _settingPanelStateHandler;
    private readonly IStrokeVisibilityStateHandler _strokeVisibilityHandler;
    private bool _isEnableFadeInk = false;
    private int _timerMiliSecond = 500;

    public StrokeVisibilityViewModel(IStrokeVisibilityStateHandler strokeVisibilityHandler, ISettingPanelStateHandler settingPanelStateHandler)
    {
        _settingPanelStateHandler = settingPanelStateHandler;
        _strokeVisibilityHandler = strokeVisibilityHandler;
        _strokeVisibilityHandler.PropertyChanged += OnStateChanged;
    }

    public bool IsEnableFadeInk
    {
        get => _isEnableFadeInk;
        set
        {
            if (_isEnableFadeInk != value)
            {
                _isEnableFadeInk = value;
                OnPropertyChanged(nameof(IsEnableFadeInk));
            }
        }
    }

    public int TimerMiliSecond
    {
        get => _timerMiliSecond;
        set
        {
            if (_timerMiliSecond != value)
            {
                _timerMiliSecond = value;
                OnPropertyChanged(nameof(TimerMiliSecond));
            }
        }
    }

    public bool IsHideAll => _strokeVisibilityHandler.IsHideAll;

    public ICommand EnableFadeInk => new RelayCommand(EnableOrDisableFadeInk);

    public ICommand SetTimerCommand => new RelayCommand<string>(SetTimer);

    public ICommand HideAllCommand => new RelayCommand(_strokeVisibilityHandler.HideAll);

    private void EnableOrDisableFadeInk() => IsEnableFadeInk = !IsEnableFadeInk;

    private void SetTimer(string milisecond)
    {
        TimerMiliSecond = int.Parse(milisecond);
        _settingPanelStateHandler.HideAllPanels();
    }
}
