namespace Riter.ViewModel;
public class SettingPanelViewModel : BaseViewModel
{
    private readonly ISettingPanelStateHandler _settingPanelStateHandler;

    public SettingPanelViewModel(ISettingPanelStateHandler settingPanelStateHandler)
    {
        _settingPanelStateHandler = settingPanelStateHandler;
        _settingPanelStateHandler.PropertyChanged += OnStateChanged;
    }

    public Visibility SettingPanelVisibility => _settingPanelStateHandler.SettingPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public Visibility BrushPanelVisibility => _settingPanelStateHandler.BrushPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public Visibility ColorPanelVisibility => _settingPanelStateHandler.ColorPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public Visibility ShapePanelVisibility => _settingPanelStateHandler.ShapePanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public Visibility TimerPanelVisibility => _settingPanelStateHandler.TimerPanelVisibility ? Visibility.Visible : Visibility.Hidden;

    public bool SettingButtonClicked => _settingPanelStateHandler.SettingButtonClicked;

    public ICommand ToggleSettingsPanelCommand => new RelayCommand(_settingPanelStateHandler.ToggleSettingsPanel);

    public ICommand ShowBrushSettingsPanelCommand => new RelayCommand<string>(_settingPanelStateHandler.ToggleBrushSettingsPanel);

    public ICommand ShowShapePanelCommand => new RelayCommand<string>(_settingPanelStateHandler.ToggleShapePanel);

    public ICommand ShowTimerPanelCommand => new RelayCommand<string>(_settingPanelStateHandler.ToggleTimerPanel);

    public ICommand ShowColorPanelCommand => new RelayCommand(_settingPanelStateHandler.ToggleColorPanel);
}
